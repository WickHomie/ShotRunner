using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] GameObject exitWindow;
    [SerializeField] private GameObject anticlicker;

    public void OpenExitWindow()
    {
        exitWindow.SetActive(true);
        anticlicker.SetActive(true);
    }

    public void CloseExitWindow()
    {
        exitWindow.SetActive(false);
        anticlicker.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
