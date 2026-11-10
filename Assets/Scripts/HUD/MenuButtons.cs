using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject rulesPanel;
    [SerializeField] private GameObject themesPanel;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private IOManager ioManager;
    [SerializeField] private ThemeManager themeManager;
    [SerializeField] private TextMeshProUGUI themesLabel;
    [SerializeField] private Image themesOutline;
    [SerializeField] private float themesCycleTime = 0.45f;
    [SerializeField] private Image[] accentSwatches;
    [SerializeField] private Image[] markSwatches;
    [SerializeField] private TextMeshProUGUI[] themeNames;

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        resumeButton.SetActive(!ioManager.gameOver);
        StopAllCoroutines();
        StartCoroutine(CycleThemesButton());
    }

    public void BackToGame()
    {
        menuPanel.SetActive(false);
        StopAllCoroutines();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowRules()
    {
        rulesPanel.SetActive(true);
        BackToGame();
    }

    public void ShowThemes()
    {
        themesPanel.SetActive(true);
        RefreshThemes();
        BackToGame();
    }

    public void SelectTheme(int index)
    {
        themeManager.SetTheme(index);
        RefreshThemes();
    }

    public void BackToMenu()
    {
        rulesPanel.SetActive(false);
        themesPanel.SetActive(false);
        ShowMenu();
    }

    private void RefreshThemes()
    {
        Palette palette = themeManager.palette;

        for (int i = 0; i < themeNames.Length; i++)
        {
            accentSwatches[i].color = palette.GetAccent(i, themeManager.isDark);
            markSwatches[i].color = palette.GetMark(i, themeManager.isDark);
            themeNames[i].text = palette.themes[i].name + (i == themeManager.themeIndex ? " *" : "");
        }
    }

    private IEnumerator CycleThemesButton()
    {
        Palette palette = themeManager.palette;
        List<Color> colors = new List<Color>();

        for (int i = 0; i < palette.themes.Length; i++)
        {
            colors.Add(palette.GetAccent(i, themeManager.isDark));
            colors.Add(palette.GetMark(i, themeManager.isDark));
        }

        int step = 0;

        while (true)
        {
            Color color = colors[step % colors.Count];
            themesLabel.color = color;
            themesOutline.color = color;
            step++;
            yield return new WaitForSeconds(themesCycleTime);
        }
    }
}
