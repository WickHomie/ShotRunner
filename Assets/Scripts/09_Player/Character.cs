using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    private Animator animator;

    public void Initialize()
    {
        Debug.Log("Инициализация персонажа");

        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Аниматор не найден");
            return;
        }

        /* string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Main Menu":
                animator.Play("Idle");
                animator.applyRootMotion = true;
                break;

            case "Game Scene":
                animator.Play("Idle_RTR");
                animator.applyRootMotion = false;
                break;

            default:
                animator.Play("Idle");
                break;
        } */
    }

    public Transform GetWeaponBone()
    {
        return transform.Find("root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/socket_r");
    }
}
