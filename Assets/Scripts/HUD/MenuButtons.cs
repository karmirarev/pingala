using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject rulesPanel;

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    public void BackToGame()
    {
        menuPanel.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowRules()
    {
        rulesPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void BackToMenu()
    {
        rulesPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
