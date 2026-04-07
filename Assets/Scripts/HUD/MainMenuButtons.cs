using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject guidePanel;
    [SerializeField] private GameObject gamePanel;

    public void NewGame()
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ShowGuide()
    {
        guidePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        guidePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
