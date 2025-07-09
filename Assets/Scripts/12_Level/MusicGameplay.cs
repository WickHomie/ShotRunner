using System.Collections;
using UnityEngine;

public class MusicGameplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private float volume = 1f; 
    [SerializeField] private float fadeDuration = 2.0f; 
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip mainMusicClip;
    [SerializeField] private AudioClip stopMusicClip;

    private Movement player;

    private bool isMusicPlaying;


    private void Start()
    {
        player = FindFirstObjectByType<Movement>();

        StartCoroutine(FadeIn());
    }

    private void Update()
    {

        if (player.isDead && isMusicPlaying)
        {
            audioSource.clip = stopMusicClip;
            audioSource.loop = false;
            audioSource.Play();

            isMusicPlaying = false;
        }
    }

    private IEnumerator FadeIn()
    {
        audioSource.clip = mainMusicClip;
        audioSource.Play();
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volume, timer / fadeDuration);
            yield return null;
        }
        audioSource.volume = volume;

        isMusicPlaying = true;
    }
}
