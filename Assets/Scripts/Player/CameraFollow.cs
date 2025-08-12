using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    
    [Header("Screen Settings")]
    [SerializeField] private float minOrthoSize = 4f;
    [SerializeField] private float maxOrthoSize = 8f;
    [SerializeField] private float zoomSpeed = 2f;
    
    private Camera cam;
    private float targetZoom;

    void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
    }

    void LateUpdate()
    {
        if (target == null) return;
        
        // Плавное перемещение к цели
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position, 
            targetPosition, 
            smoothSpeed * Time.deltaTime
        );
        
        transform.position = smoothedPosition;
        
        // Обработка зума (закоментируйте, если зум не нужен)
        HandleZoom();
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            targetZoom -= scroll * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minOrthoSize, maxOrthoSize);
        }
        
        cam.orthographicSize = Mathf.Lerp(
            cam.orthographicSize, 
            targetZoom, 
            zoomSpeed * Time.deltaTime
        );
    }

    public void SetTarget(Transform newTarget) => target = newTarget;
}
