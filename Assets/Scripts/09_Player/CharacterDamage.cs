using System.Collections;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    [SerializeField] CameraShake cameraShake;

    public int maxPlayerHealth = 100;
    public int currentPlayerHealth;

    private Movement movement;

    void Start()
    {
        movement = GetComponent<Movement>();
        currentPlayerHealth = maxPlayerHealth;
    }

    public void TakePlayerDamage(int damage)
    {
        currentPlayerHealth -= damage;

        cameraShake.ShakeCamera();

        if (currentPlayerHealth <= 0)
        {
            movement.isDead = true;
            movement.animator.Play("Knockdown");

            cameraShake.ShakeCamera();

            StartCoroutine(LoadScene(3f));
        }
    }

    private IEnumerator LoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneLoader.Instance.LoadScene(0);
    }
}
