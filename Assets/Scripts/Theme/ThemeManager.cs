using UnityEngine;

[DefaultExecutionOrder(-100)]
public class ThemeManager : MonoBehaviour
{
    [field: SerializeField] public Palette palette { get; private set; }
    [SerializeField] private ThemeColor dayLabel;
    [SerializeField] private ThemeColor nightLabel;

    public int themeIndex { get; private set; }
    public bool isDark { get; private set; }

    private void Awake()
    {
        themeIndex = Mathf.Clamp(PlayerPrefs.GetInt("pingala-theme", 0), 0, palette.themes.Length - 1);
        isDark = PlayerPrefs.GetString("pingala-mode", "light") == "dark";
    }

    private void Start()
    {
        Apply();
    }

    public Color GetColor(ColorRole role)
    {
        return palette.GetColor(role, themeIndex, isDark);
    }

    public void ToggleMode()
    {
        isDark = !isDark;
        PlayerPrefs.SetString("pingala-mode", isDark ? "dark" : "light");
        Apply();
    }

    public void SetTheme(int index)
    {
        themeIndex = index;
        PlayerPrefs.SetInt("pingala-theme", index);
        Apply();
    }

    private void Apply()
    {
        dayLabel.SetRole(isDark ? ColorRole.Faint : ColorRole.Ink);
        nightLabel.SetRole(isDark ? ColorRole.Ink : ColorRole.Faint);

        ThemeColor[] colors = FindObjectsByType<ThemeColor>();

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i].Apply();
        }
    }
}
