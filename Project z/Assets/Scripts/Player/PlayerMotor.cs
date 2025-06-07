using System;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private PlayerAnimation playerAnimation;
    private Vector3 playerVelocity;
    private Vector3 move_history;
    private Vector3 move_dir;
    private bool isMoving;
    private bool isGrounded;
    private bool lerpCrouch;
    private bool crouching;
    private bool sprinting;
    private float crouchTimer;
    public float maxWalkSpeed = 5f;
    private float speed = 0f;
    public float maxSprintSpeed = 10f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float acceleration = 5f;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private Vector3 curentCoords
    {
        get { return new Vector3(transform.position.x, transform.position.y, transform.position.z); }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsSprinting()
    {
        return sprinting;
    }

    public Transform GetCurrentFacingDirection()
    {
        return transform;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    // LateUpdate is when the mosue events are called
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if (p >= 1)
            {
                lerpCrouch = false;
                crouchTimer = 0;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        move_dir = Vector3.zero;
        move_dir.x = input.x;
        move_dir.z = input.y;

        move_dir = move_dir == Vector3.zero ? move_history : move_dir;

        float targetSpeed = sprinting ? maxSprintSpeed : maxWalkSpeed;

        if (input.x == 0 && input.y == 0)
        {
            speed = Mathf.MoveTowards(speed, 0, acceleration * Time.deltaTime);
        }
        else
        {
            speed = Mathf.MoveTowards(speed, targetSpeed, acceleration * Time.deltaTime);
        }

        //Inform to the PlayerAnimation Listener
        ProcessMoveEvent?.Invoke(this, new MoveEventArgs(move_dir * speed));

        controller.Move(transform.TransformDirection(move_dir) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -1f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        move_history = move_dir;

        //updating isMoving
        isMoving = (input.x != 0 || input.y != 0) ? true : false;

        // Debug.Log("Player Velocity Rounded: " + move_dir * speed);
    }

    public void Jump()
    {
        float time_in_air = Mathf.Sqrt(-2 * jumpHeight / gravity);
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // Animation Event
            playerAnimation.Jump();
        }


    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
    }

    #region Animation Events
    public event EventHandler<MoveEventArgs> ProcessMoveEvent;

    public class MoveEventArgs : EventArgs
    {
        public Vector3 moveDir;
        public float currentPlayerSpeed;

        public MoveEventArgs(Vector3 moveDir)
        {
            this.moveDir = moveDir;
            currentPlayerSpeed = moveDir.magnitude;
        }
    }
    #endregion
}
