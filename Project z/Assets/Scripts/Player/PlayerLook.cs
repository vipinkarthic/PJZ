using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float downClamp = 55f;
    [SerializeField] private float upClamp = 55f;
    public Camera cam;
    private float xRotation = 0f;
    public float xSenstivity = 30f;
    public float ySenstivity = 30f;

    // private void Start()
    // {
    //     Cursor.lockState = CursorLockMode.Locked;
    //     Cursor.visible = false;
    // }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= mouseY * ySenstivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, upClamp, downClamp);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(mouseX * Time.deltaTime * xSenstivity * Vector3.up);
    }
}
