using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Number : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] IOManager ioManager;
    [SerializeField] private Image markDot;
    [SerializeField] private Image ring;
    [SerializeField] private float longPressTime = 0.5f;
    [SerializeField] private float ringTime = 0.35f;
    public int digit;

    private Button button;
    private bool isMarked;
    private bool isHolding;
    private bool wasLongPress;
    private float holdTimer;

    private void Awake()
    {
        button = GetComponent<Button>();
        markDot.enabled = false;
        ring.fillAmount = 0;
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
        if (wasLongPress) return;

        ioManager.HandleNumberInput(digit);
    }

    public void SetUsed(bool used)
    {
        button.interactable = !used;
        StopAllCoroutines();

        if (used)
        {
            StartCoroutine(DrawRing());
        }
        else
        {
            ring.fillAmount = 0;
        }
    }

    private IEnumerator DrawRing()
    {
        float t = 0;

        while (t < 1f)
        {
            t = Mathf.Min(t + Time.deltaTime / ringTime, 1f);
            ring.fillAmount = 1f - (1f - t) * (1f - t);
            yield return null;
        }
    }

    private void ToggleMark()
    {
        isMarked = !isMarked;
        markDot.enabled = isMarked;
    }
}
