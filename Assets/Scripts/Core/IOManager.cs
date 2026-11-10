using TMPro;
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
    [SerializeField] private RectTransform rowArrow;
    [SerializeField] private TextMeshProUGUI triesText;
    [SerializeField] private ThemeColor submitLabel;
    [SerializeField] private TextMeshProUGUI resultNumberText;

    private Row[] inputRows;
    private Row[] outputRows;
    private Number[] numberKeys;
    private int currentRowIndex;
    private int currentColumnIndex;
    private int correctNumberCounter;
    private int correctPlacementCounter;
    private int[] hiddenNumber;
    public bool gameOver { get; private set; }

    void Start()
    {
        GenerateHiddenNumber();
        Refresh();
    }

    private void Awake()
    {
        inputRows = inputField.GetComponentsInChildren<Row>();
        outputRows = outputField.GetComponentsInChildren<Row>();
        numberKeys = FindObjectsByType<Number>();
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
            SetKeyUsed(inputNumber, true);
            Refresh();
        }
    }

    public void HandleDelete()
    {
        if (gameOver) return;

        if (currentColumnIndex > 0)
        {
            currentColumnIndex--;
            Tile tile = inputRows[currentRowIndex].tiles[currentColumnIndex];
            SetKeyUsed(tile.digit, false);
            tile.ClearLastDigit();
            Refresh();
        }
    }

    private void SetKeyUsed(int digit, bool used)
    {
        for (int i = 0; i < numberKeys.Length; i++)
        {
            if (numberKeys[i].digit == digit)
            {
                numberKeys[i].SetUsed(used);
            }
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

            for (int i = 0; i < numberKeys.Length; i++)
            {
                numberKeys[i].SetUsed(false);
            }
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

        Refresh();
    }

    private void Refresh()
    {
        for (int i = 0; i < inputRows.Length; i++)
        {
            for (int j = 0; j < inputRows[i].tiles.Length; j++)
            {
                inputRows[i].tiles[j].SetPlaceholder(!gameOver && i == currentRowIndex && j >= currentColumnIndex);
            }
        }

        rowArrow.gameObject.SetActive(!gameOver);
        rowArrow.anchoredPosition = new Vector2(rowArrow.anchoredPosition.x, -currentRowIndex * rowArrow.rect.height);

        int shownTry = Mathf.Min(currentRowIndex + (gameOver ? 0 : 1), inputRows.Length);
        triesText.text = "TRY " + shownTry.ToString("00") + "/" + inputRows.Length;

        submitLabel.SetRole(currentColumnIndex == 4 && !gameOver ? ColorRole.Ink : ColorRole.Faint);
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

        resultNumberText.text = "the number was " + string.Join("", hiddenNumber);
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