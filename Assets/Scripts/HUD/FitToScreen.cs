using UnityEngine;

public class FitToScreen : MonoBehaviour
{
    [SerializeField] private Vector2 size = new Vector2(1080, 2640);
    [SerializeField] private float padding = 48f;

    private RectTransform parent;

    private void Awake()
    {
        parent = transform.parent as RectTransform;
    }

    private void Update()
    {
        float width = (parent.rect.width - padding * 2) / size.x;
        float height = (parent.rect.height - padding * 2) / size.y;
        transform.localScale = Vector3.one * Mathf.Min(width, height);
    }
}
