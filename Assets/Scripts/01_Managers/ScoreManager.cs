using TMPro;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreboardText;
    public int score = 0;

    private const int maxScores = 3;
    private const string scoreKeyPrefix = "HighScore";

    public void AddScore(float amount)
    {
        score += (int)amount;
        scoreboardText.text = score.ToString();
    }

    public void SaveScore()
    {
        int[] highScores = new int[maxScores];
        for (int i = 0; i < maxScores; i++)
        {
            highScores[i] = PlayerPrefs.GetInt(scoreKeyPrefix + i, 0);
        }

        highScores = highScores.Append(score).ToArray();

        highScores = highScores.OrderByDescending(s => s).Take(maxScores).ToArray();


        for (int i = 0; i < maxScores; i++)
        {
            PlayerPrefs.SetInt(scoreKeyPrefix + i, highScores[i]);
        }

        PlayerPrefs.Save();

        score = 0;
    }

    public void ClearHighScores()
    {
        for (int i = 0; i < maxScores; i++)
        {
            PlayerPrefs.DeleteKey(scoreKeyPrefix + i);
        }
        PlayerPrefs.Save();
    }

}
