using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject timeLine;
    [SerializeField] GameObject mainPanel;

    public Vector3 cameraPositionEnd;
    public Quaternion cameraRotationEnd;

    public float timeToChangeScene = 1.6f;

    new CameraMovement camera;

    void Start()
    {
        camera = FindFirstObjectByType<CameraMovement>();
    }

    public void PlayGame()
    {
        StartCoroutine(WaitChangeScene(timeToChangeScene));
        timeLine.SetActive(false);
        mainPanel.SetActive(false);
        CameraMove();
    }

    public void Shop()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void CameraMove()
    {
        camera.MoveCameraTo(cameraPositionEnd, cameraRotationEnd);
    }

    private IEnumerator WaitChangeScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(1);
    }
}
