using UnityEngine;
using UnityEngine.UI;

public enum ColorRole { Paper, Ink, Faint, Grid, Line, Accent, Mark, OnInk }

[RequireComponent(typeof(Graphic))]
public class ThemeColor : MonoBehaviour
{
    [SerializeField] private ColorRole role;
    [SerializeField] private float alpha = 1f;

    private ThemeManager themeManager;
    private Graphic graphic;
    private bool pressed;

    private void OnEnable()
    {
        Apply();
    }

    public void Apply()
    {
        if (themeManager == null)
        {
            themeManager = FindAnyObjectByType<ThemeManager>();
            graphic = GetComponent<Graphic>();
        }

        Color color = themeManager.GetColor(pressed ? ColorRole.OnInk : role);
        color.a *= alpha;
        graphic.color = color;
    }

    public void SetRole(ColorRole newRole)
    {
        role = newRole;
        Apply();
    }

    public void SetPressed(bool isPressed)
    {
        pressed = isPressed;
        Apply();
    }
}
