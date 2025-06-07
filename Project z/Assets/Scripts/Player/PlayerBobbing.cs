using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private float walkBobFrequency;
    [SerializeField] private float sprintBobFrequency;
    [SerializeField] private float bobAmplitude;
    private PlayerMotor playerMotor;

    private float timer = 0f;
    private Vector3 initialPosition;

    void Start()
    {
        playerMotor = GetComponentInParent<PlayerMotor>();
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Debug.Log(playerMotor.IsMoving());
        if (playerMotor.IsMoving())
        {
            float bobFrequency = playerMotor.IsSprinting() ? sprintBobFrequency : walkBobFrequency;
            timer += Time.deltaTime * bobFrequency;
            float newY = initialPosition.y + Mathf.Sin(timer) * bobAmplitude;
            transform.localPosition = new Vector3(initialPosition.x, newY, initialPosition.z);
        }
        else
        {
            timer = 0f;
            float bobFrequency = playerMotor.IsSprinting() ? sprintBobFrequency : walkBobFrequency;
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * bobFrequency);
        }
    }
}
