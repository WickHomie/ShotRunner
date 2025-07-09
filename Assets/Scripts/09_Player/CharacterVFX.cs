using System.Collections;
using UnityEngine;

public class CharacterVFX : MonoBehaviour
{
    [SerializeField] ParticleSystem enemyCoinParticlePref;
    [SerializeField] ParticleSystem vfxDamageCooldownPref;

    private ParticleSystem addEnemyCoins;
    private ParticleSystem vfxDamageCooldown;

    public void AddEnemyCoins()
    {
        addEnemyCoins = Instantiate(enemyCoinParticlePref, transform.position, Quaternion.identity, transform.parent);
        addEnemyCoins.Play();

        Destroy(addEnemyCoins.gameObject, 1.5f);
    }

    public void DamageCooldownVFX()
    {
        vfxDamageCooldown = Instantiate(vfxDamageCooldownPref, transform.position, Quaternion.identity, transform.parent);
        vfxDamageCooldown.Play();
        StartCoroutine(DestroyAfterParticles(vfxDamageCooldown));
    }

    IEnumerator DestroyAfterParticles(ParticleSystem ps)
    {
        yield return new WaitForSecondsRealtime(3f);
        ps.Stop();

        Destroy(ps.gameObject, 2f);
    }
}
