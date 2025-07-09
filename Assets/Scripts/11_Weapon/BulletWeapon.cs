using UnityEngine;

public class BulletWeapon : Bullet
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private AudioSource shotSound;

    [SerializeField] private float shotSpeed = 20f;
    [SerializeField] private float shotCount = 1f;

    private float nextShotTime = 0f;

    public void Fire()
    {
        if (CanShoot())
        {
            GameObject bulletObj = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Shot(shotSpeed);

                shotSound.pitch = Random.Range(0.9f, 1.1f);
                shotSound.Play();
            }
            nextShotTime = Time.time + 1f / shotCount;
        }
    }

    protected bool CanShoot()
    {
        return Time.time >= nextShotTime;
    }

}
