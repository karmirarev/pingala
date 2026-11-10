using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject placeholder;
    [SerializeField] private Image fill;
    public int digit;

    public void SetDigit(int newDigit)
    {
        this.digit = newDigit;
        text.text = newDigit.ToString();
        SetPlaceholder(false);

        if (fill != null)
        {
            fill.enabled = true;
        }
    }

    public void ClearLastDigit()
    {
        this.digit = 0;
        text.text = "";
    }

    public void SetPlaceholder(bool show)
    {
        if (placeholder != null)
        {
            placeholder.SetActive(show);
        }
    }
}
