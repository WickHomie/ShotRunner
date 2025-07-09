using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private ParticleSystem deathCrashParticle;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UnityEngine.Audio.AudioMixerGroup sfxMixerGroup;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip damageClip;
    [SerializeField] private AudioClip deathBulletClip;
    [SerializeField] private AudioClip deathCrushClip;

    [Header("Setting")]
    [SerializeField] private int damage = 10;
    [SerializeField] int coinAmound = 0;

    public int maxEnemyHealth = 100;
    public int currentEnemyHealth;

    private ParticleSystem enemyCrashDead;
    private Vector3 deathBulletPartPos;
    private CharacterVFX character;
    private LevelGenerator levelGenerator;


    private void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
        character = FindAnyObjectByType<CharacterVFX>();
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        deathBulletPartPos = new Vector3(0, 2, 0);
    }

    public void TakeEnemyDamage(int damage)
    {
        currentEnemyHealth -= damage;

        float minPitch = 0.6f;
        float maxPitch = 1.2f;
        float healthPercent = Mathf.Clamp01((float)currentEnemyHealth / maxEnemyHealth);
        audioSource.pitch = Mathf.Lerp(maxPitch, minPitch, healthPercent);

        audioSource.clip = damageClip;
        audioSource.Play();

        if (currentEnemyHealth <= 0)
        {
            levelGenerator.wallet.AddCoins(coinAmound);

            enemyCrashDead = Instantiate(deathCrashParticle, transform.position + deathBulletPartPos, Quaternion.identity, transform.parent);
            enemyCrashDead.Play();

            character.AddEnemyCoins();

            GameObject tempGO = DeathSound(deathBulletClip, 1f, 0.7f);
            Destroy(tempGO, deathBulletClip.length);

            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        CharacterDamage character = other.GetComponent<CharacterDamage>();
        if (character != null)
        {
            enemyCrashDead = Instantiate(deathCrashParticle, transform.position + deathBulletPartPos, Quaternion.identity, transform.parent);
            enemyCrashDead.Play();

            character.TakePlayerDamage(damage);
        }

        if (other.CompareTag("Player"))
        {
            GameObject tempGO = DeathSound(deathCrushClip, 1f, 1f);
            Destroy(tempGO, deathCrushClip.length);

            Destroy(gameObject);
        }

    }


    private GameObject DeathSound(AudioClip audioClip, float volume, float pitch)
    {
        audioSource.clip = audioClip;
        audioSource.pitch = 1f;

        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = transform.position;

        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = audioClip;
        aSource.volume = volume;
        aSource.pitch = pitch;
        aSource.spatialBlend = 0.6f;
        aSource.outputAudioMixerGroup = sfxMixerGroup;
        aSource.Play();
        return tempGO;
    }

}
