using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void NextLevel(string nameLevel)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nameLevel);
        GameAnalyticsInit.Instance.OnLevelComlete(nameLevel);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
