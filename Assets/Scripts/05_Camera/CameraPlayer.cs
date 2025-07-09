using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraPlayer : MonoBehaviour
{
    public float moveCamSpeed;
    public float rotateCamSpeed;

    private Vector3 targetPosition = new Vector3(0, 0, 0);
    private Quaternion targetRotation = Quaternion.Euler(0,0,0);

    public Transform player;

    [SerializeField] GameObject cinemachineCamera;

    private LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();      
        StartCoroutine(GlideToPlayerAndAttach());
    }


    private IEnumerator GlideToPlayerAndAttach()
    {
        while ((Vector3.Distance(transform.position, targetPosition) > 0.01f || Quaternion.Angle(transform.rotation, targetRotation) > 0.01f) && !levelGenerator.readyToStart)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveCamSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateCamSpeed);
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        cinemachineCamera.SetActive(true);
    
    }
}
