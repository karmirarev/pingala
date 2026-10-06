using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int digit;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetDigit(int newDigit)
    {
        this.digit = newDigit;
        text.text = newDigit.ToString();
    }

    public void ClearLastDigit()
    {
        this.digit = 0;
        text.text = "";
    }
}
