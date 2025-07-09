using UnityEngine;
using UnityEngine.Audio;

public class SoundVolumeSettings : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider musicSlider;
    [SerializeField] private UnityEngine.UI.Slider sfxSlider;

    public AudioMixerGroup mixer;

    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private void Start()
    {
        LoadVolumes();
    }

    public void ChangeVolumeMusic(float volume)
    {
        musicVolume = volume;
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    public void ChangeVolumeSFX(float volume)
    {
        sfxVolume = volume;
        mixer.audioMixer.SetFloat("SFXVolume", Mathf.Lerp(-80, 0, volume));
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    private void LoadVolumes()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, musicVolume));
        mixer.audioMixer.SetFloat("SFXVolume", Mathf.Lerp(-80, 0, sfxVolume));

        if (musicSlider != null)
            musicSlider.value = musicVolume;
        if (sfxSlider != null)
            sfxSlider.value = sfxVolume;
    }

}
