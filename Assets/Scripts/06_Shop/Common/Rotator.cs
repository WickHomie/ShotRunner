using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField, Range(0, 10)] private float autoRotationSpeed = 1f;
    [SerializeField] private float mouseRotationSpeed = 5f;
    [SerializeField] private float startRotation = 75f;

    [Header("Rotation Zone")]
    [SerializeField] private RectTransform rotationZone;
    
    private float currentRotationY = 0f;
    private bool isRotatingManually = false;
    private Vector2 lastMousePosition;
    private Transform targetTransform;
    private Camera mainCamera;

    void Update()
    {
        if (targetTransform == null) return;

        HandleMouseInput();
        
        if (!isRotatingManually)
        {
            AutoRotate();
            ApplyRotation();
        }
    }

    private void AutoRotate()
    {
        currentRotationY -= Time.deltaTime * autoRotationSpeed;
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (rotationZone != null && !IsMouseInRotationZone())
                return;

            isRotatingManually = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotatingManually = false;
        }

        if (isRotatingManually && Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;
            Vector2 delta = currentMousePos - lastMousePosition;
            lastMousePosition = currentMousePos;

            currentRotationY -= delta.x * mouseRotationSpeed * Time.deltaTime;
            ApplyRotation();
        }
    }

    private bool IsMouseInRotationZone()
    {
        if (rotationZone == null) return true;
        
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rotationZone, 
            Input.mousePosition, 
            mainCamera, 
            out localMousePosition);
            
        return rotationZone.rect.Contains(localMousePosition);
    }

    private void ApplyRotation()
    {
        targetTransform.rotation = Quaternion.Euler(0, currentRotationY, 0);
    }

    public void ResetRotation()
    {
        currentRotationY = startRotation;
        ApplyRotation();
    }

    public void SetTarget(Transform newTarget)
    {
        targetTransform = newTarget;
        ResetRotation();
    }

}
