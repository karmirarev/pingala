using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject MenuPanel;

    void Start()
    {
        MenuPanel.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        if (MenuPanel != null)
        {
            MenuPanel.SetActive(!MenuPanel.activeSelf);
        }
    }

    public void BackToGame()
    {
        MenuPanel.SetActive(false);
    }

    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
