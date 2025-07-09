using UnityEngine;
using UnityEngine.SceneManagement;

public class RebootData : MonoBehaviour
{
    [SerializeField] MainMenuBootstrap mainMenuBootstrap;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject rebootWindow;
    [SerializeField] GameObject settingWindow;

    public void OpenRebootWindow()
    {
        rebootWindow.SetActive(true);
        settingWindow.SetActive(false);
    }

    public void CloseRebootWindow()
    {
        rebootWindow.SetActive(false);
        settingWindow.SetActive(true);
    }

    public void RebootDataSave()
    {
        mainMenuBootstrap.RebootSave();
        scoreManager.ClearHighScores();
        rebootWindow.SetActive(false);

         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
