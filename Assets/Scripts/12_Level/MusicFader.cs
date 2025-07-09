using System.Collections;
using UnityEngine;

public class MusicFader : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float volume = 1f; 
    [SerializeField] private float fadeDuration = 2.0f; 
    [SerializeField] private AudioClip musicClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.volume = 0f;
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        audioSource.Play();
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volume, timer / fadeDuration);
            yield return null;
        }
        audioSource.volume = volume;
    }

}
