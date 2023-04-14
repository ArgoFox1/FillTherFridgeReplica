using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Support()
    {
        string url = "https://www.youtube.com/channel/UCGPS3t2FfqaljxiaNs9YaSA";
        Application.OpenURL(url);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
