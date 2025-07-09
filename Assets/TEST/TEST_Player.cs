using UnityEngine;

public class TEST_Player : MonoBehaviour
{
    private BulletWeapon bulletWeapon;

    void Start()
    {
        bulletWeapon = FindFirstObjectByType<BulletWeapon>();
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            bulletWeapon.Fire();   
        }
    }
}
