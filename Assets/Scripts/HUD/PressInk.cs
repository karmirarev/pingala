using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressInk : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private Image fill;
    [SerializeField] private ThemeColor[] labels;

    private Selectable selectable;

    private void Awake()
    {
        selectable = GetComponent<Selectable>();
        fill.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (selectable.interactable)
        {
            SetPressed(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetPressed(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetPressed(false);
    }

    private void SetPressed(bool pressed)
    {
        fill.enabled = pressed;

        for (int i = 0; i < labels.Length; i++)
        {
            labels[i].SetPressed(pressed);
        }
    }
}
