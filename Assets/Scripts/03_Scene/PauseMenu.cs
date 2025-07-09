using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] private GameObject panel;
    [SerializeField] private Popup popup;
    [SerializeField] private CanvasGroup bodyAlphaGroup;
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] ScoreManager scoreManager;

    public AudioMixerGroup mixer;

    public AudioMixerSnapshot basicVolume;
    public AudioMixerSnapshot pauseVolume;

    public void Pause()
    {
        pausePanel.SetActive(true);
        panel.SetActive(false);
        Time.timeScale = 0;
        levelGenerator.readyToStart = false;
        pauseVolume.TransitionTo(0.2f);
    }

    public void MainMenu()
    {
        basicVolume.TransitionTo(0.5f);
        scoreManager.SaveScore();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        
    }

    public void Resume()
    {
        popup.Hide(OnPopupHidden);
        basicVolume.TransitionTo(0.5f);
    }

    private void OnPopupHidden()
    {
        Time.timeScale = 1;
        levelGenerator.readyToStart = true;
        pausePanel.SetActive(false);
        panel.SetActive(true);
        bodyAlphaGroup.alpha = 1;
    }
}
