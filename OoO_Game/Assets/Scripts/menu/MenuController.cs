using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("StartingBase");
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlayMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
