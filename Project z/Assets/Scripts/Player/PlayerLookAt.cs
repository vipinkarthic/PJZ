using UnityEngine;

public class CameraForwardLookAt : MonoBehaviour
{
    public Camera sourceCamera;
    public Transform lookAtTarget;
    public float maxDistance = 20f;
    private LayerMask collisionMask;


    void Awake()
    {
        collisionMask = ~(1 << 17);
    }

    void Reset()
    {
        // Auto-assign main camera if none provided
        if (sourceCamera == null && Camera.main != null)
        {
            sourceCamera = Camera.main;
        }
    }

    void FixedUpdate()
    {
        lookAtTarget.position = sourceCamera.transform.position + sourceCamera.transform.forward * maxDistance;

        //Debug ray to visualize the look-at target
        Debug.DrawLine(sourceCamera.transform.position, lookAtTarget.position, Color.red);
    }
}
