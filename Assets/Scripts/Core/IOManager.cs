using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class IOManager : MonoBehaviour
{
    [SerializeField] private GameObject inputField;
    [SerializeField] private GameObject outputField;
    [SerializeField] private GameObject WinningLosingPanel;
    [SerializeField] private GameObject WinningText;
    [SerializeField] private GameObject LosingText;
    [SerializeField] private Tile[] hiddenTiles;

    private Row[] inputRows;
    private Row[] outputRows;
    private int currentRowIndex;
    private int currentColumnIndex;
    private int correctNumberCounter;
    private int correctPlacementCounter;
    private int[] hiddenNumber;
    private bool gameOver;

    void Start()
    {
        GenerateHiddenNumber();
    }

    private void Awake()
    {
        inputRows = inputField.GetComponentsInChildren<Row>();
        outputRows = outputField.GetComponentsInChildren<Row>();
    }

    public void GenerateHiddenNumber()
    {
        System.Random random = new System.Random();
        hiddenNumber = new int[4];

        for (int i = 0; i < hiddenNumber.Length; i++)
        {
            int number;
            bool isDuplicate;

            do
            {
                number = random.Next(0, 10);
                isDuplicate = false;

                for (int j = 0; j < i; j++)
                {
                    if (hiddenNumber[j] == number)
                    {
                        isDuplicate = true;
                        break;
                    }
                }
            } while (isDuplicate);

            hiddenNumber[i] = number;
        }
    }

    public void HandleNumberInput(int inputNumber)
    {
        if (gameOver) return;

        if (currentColumnIndex >= inputRows[currentRowIndex].tiles.Length)
        {
            return;
        }

        bool isDuplicate = false;

        for (int i = 0; i < currentColumnIndex; i++)
        {
            if (inputNumber == inputRows[currentRowIndex].tiles[i].digit && currentColumnIndex < inputRows[currentRowIndex].tiles.Length)
            {
                isDuplicate = true;
            }
        }

        if (isDuplicate == false)
        {
            inputRows[currentRowIndex].tiles[currentColumnIndex].SetDigit(inputNumber);
            currentColumnIndex++;
        }
    }

    public void HandleDelete()
    {
        if (gameOver) return;

        if (currentColumnIndex > 0)
        {
            currentColumnIndex--;
            inputRows[currentRowIndex].tiles[currentColumnIndex].ClearLastDigit();
        }
    }

    public void HandleSubmit()
    {
        if (gameOver) return;

        if (currentColumnIndex == 4)
        {
            CompareGuess();
            outputRows[currentRowIndex].tiles[0].SetDigit(correctNumberCounter);
            outputRows[currentRowIndex].tiles[1].SetDigit(correctPlacementCounter);
            currentRowIndex++;
            currentColumnIndex = 0;
        }

        if (correctPlacementCounter == 4)
        {
            gameOver = true;
            StartCoroutine(RevealSequence(true));
        }
        else if (currentRowIndex >= inputRows.Length)
        {
            gameOver = true;
            StartCoroutine(RevealSequence(false));
        }
    }

    private IEnumerator RevealSequence(bool isWinning)
    {
        for (int i = 0; i < hiddenNumber.Length; i++)
        {
            hiddenTiles[i].SetDigit(hiddenNumber[i]);
            yield return new WaitForSeconds(0.4f);
        }

        for (int i = 0; i < hiddenTiles.Length; i++)
        {
            StartCoroutine(BounceDigit(hiddenTiles[i].transform));
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1f);

        WinningLosingPanel.SetActive(true);
        if (isWinning)
        {
            WinningText.SetActive(true);
        } else
        {
            LosingText.SetActive(true);
        }
    }

    private IEnumerator BounceDigit(Transform tile)
    {
        Vector3 start = tile.localPosition;
        Vector3 up = start + new Vector3(0, 30f, 0);

        // move up
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * 5f;
            tile.localPosition = Vector3.Lerp(start, up, t);
            yield return null;
        }

        // move back down
        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * 5f;
            tile.localPosition = Vector3.Lerp(up, start, t);
            yield return null;
        }
    }

    private void CompareGuess()
    {
        correctNumberCounter = 0;
        correctPlacementCounter = 0;

        for (int i = 0; i < inputRows[currentRowIndex].tiles.Length; i++)
        {
            if (inputRows[currentRowIndex].tiles[i].digit == hiddenNumber[i])
            {
                correctNumberCounter++;
                correctPlacementCounter++;
            } else
            {
                for (int j = 0; j < hiddenNumber.Length; j++)
                {
                    if (inputRows[currentRowIndex].tiles[i].digit == hiddenNumber[j])
                    {
                        correctNumberCounter++;
                        break;
                    }
                }
            }
        }
    }
}