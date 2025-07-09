using UnityEngine;

public class GameplayPanel : MonoBehaviour
{
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject score;
    [SerializeField] GameObject wallet;


    private void Update()
    {
        if (levelGenerator.readyToStart)
        {
            pauseButton.SetActive(true);
            score.SetActive(true);
            wallet.SetActive(true);
        }
    }
}
