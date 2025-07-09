using UnityEngine;

public class Truck : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem hitBulletPrefab;
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip hitBulletClip;

    private float nextAllowedHitTime = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (Time.time >= nextAllowedHitTime)
            {
                nextAllowedHitTime = Time.time + Random.Range(0.5f, 1f);

                Rigidbody bulletRb = other.attachedRigidbody;
                Vector3 bulletVelocity = bulletRb != null ? bulletRb.linearVelocity : Vector3.zero;
                Vector3 bulletDirection = bulletVelocity != Vector3.zero
                    ? bulletVelocity.normalized 
                    : (other.transform.position - transform.position).normalized;

                Vector3 contactPoint = other.ClosestPoint(transform.position + bulletDirection * 0.01f);

                ParticleSystem hit = Instantiate(hitBulletPrefab, contactPoint, Quaternion.LookRotation(bulletDirection), transform.parent);
                hit.Play();

                audioSource.clip = hitBulletClip;
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.Play();

                Destroy(hit.gameObject, 0.3f);

            }

        }
    }
}
