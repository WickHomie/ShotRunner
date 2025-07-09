using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    public virtual void Shot(float speedShot)
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speedShot;
            Destroy(gameObject, 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeEnemyDamage(damage);
        }

        if (!other.CompareTag("Coin") && !other.CompareTag("Chunk"))
        {
            Destroy(gameObject);
        }
    }
}
