using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Number : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] IOManager ioManager;
    [SerializeField] private Color markedColor = new Color(0.85f, 0.35f, 0.3f, 1f);
    [SerializeField] private float longPressTime = 0.5f;
    public int digit;

    private Button button;
    private Image image;
    private TextMeshProUGUI text;
    private Color normalColor;
    private bool isMarked;
    private bool isHolding;
    private bool wasLongPress;
    private float holdTimer;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        normalColor = image.color;
    }

    private void Update()
    {
        if (!isHolding) return;

        holdTimer += Time.deltaTime;

        if (holdTimer >= longPressTime)
        {
            isHolding = false;
            wasLongPress = true;
            ToggleMark();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        wasLongPress = false;
        holdTimer = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHolding = false;
    }

    public void OnNumberClick()
    {
        if (wasLongPress || isMarked) return;

        ioManager.HandleNumberInput(digit);
    }

    public void SetUsed(bool used)
    {
        button.interactable = !used;
        text.alpha = used ? 0.3f : 1f;
    }

    private void ToggleMark()
    {
        isMarked = !isMarked;
        image.color = isMarked ? markedColor : normalColor;
        button.transition = isMarked ? Selectable.Transition.None : Selectable.Transition.ColorTint;
    }
}
