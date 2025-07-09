using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    public float moveCamSpeed;
    public float rotateCamSpeed;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private bool shouldMove = false;

    private Transform camTransform;

    void Start()
    {
        camTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (shouldMove)
        {
            camTransform.position = Vector3.Lerp(camTransform.position, targetPosition, moveCamSpeed / 5 * Time.deltaTime);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, targetRotation, rotateCamSpeed / 5 * Time.deltaTime);

            if (Vector3.Distance(camTransform.position, targetPosition) < 0.01f && Quaternion.Angle(camTransform.rotation, targetRotation) < 0.5f)
            {
                camTransform.position = targetPosition;
                camTransform.rotation = targetRotation;
                shouldMove = false;
            }
        }
    }

    public void MoveCameraTo(Vector3 newPosition, Quaternion newRotation)
    {
        targetPosition = newPosition;
        targetRotation = newRotation;
        shouldMove = true;
    }
}
