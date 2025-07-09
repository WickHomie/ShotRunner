using UnityEngine;

public class RotateBullet : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 100f, 0f);

    void Update()
    {
        transform.Rotate(rotationSpeed * 10 * Time.deltaTime);
    }

}
