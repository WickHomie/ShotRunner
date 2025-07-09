using UnityEngine;

public class SoundUI : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip backButtonClip;
    [SerializeField] private AudioClip startButtonClip;

    public void ButtonClicked()
    {
        audioSource.clip = buttonClip;
        audioSource.Play();
    }

    public void BackButtonClicked()
    {
        audioSource.clip = backButtonClip;
        audioSource.Play();
    }

    public void StartButtonClicked()
    {
        audioSource.clip = startButtonClip;
        audioSource.Play();
    }
}
