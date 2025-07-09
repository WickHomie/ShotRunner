using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text top1Text;
    [SerializeField] private TMP_Text top2Text;
    [SerializeField] private TMP_Text top3Text;

    [SerializeField] private ParticleSystem top1ParticlesPref;

    private ParticleSystem top1Particles;

    private const int maxScores = 3;
    private const string scoreKeyPrefix = "HighScore";

    private int topLastScore;

    private void Start()
    {
        topLastScore = PlayerPrefs.GetInt("LastTop1Score", PlayerPrefs.GetInt(scoreKeyPrefix + "0", 0));

        DisplayHighScores();
    }



    public void DisplayHighScores()
    {
        for (int i = 0; i < maxScores; i++)
        {
            int score = PlayerPrefs.GetInt(scoreKeyPrefix + i, 0);
            string displayText = $"{score}";

            switch (i)
            {
                case 0:
                    if (top1Text != null && score > 0)
                        top1Text.text = displayText;
                    if (score > topLastScore)
                    {
                        top1Particles = Instantiate(top1ParticlesPref, top1Text.transform);
                        top1Particles.Play();
                    }
                    break;
                case 1:
                    if (top2Text != null && score > 0)
                        top2Text.text = displayText;

                    break;
                case 2:
                    if (top3Text != null && score > 0)
                        top3Text.text = displayText;

                    break;
            }

        }

        topLastScore = PlayerPrefs.GetInt(scoreKeyPrefix + "0", 0);

        PlayerPrefs.SetInt("LastTop1Score", topLastScore);
        PlayerPrefs.Save();

    }
    
}
