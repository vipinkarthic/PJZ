using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerMotor playerMotor;

    private readonly string vX = "Velocity X";
    private readonly string vZ = "Velocity Z";
    private readonly string jump = "Jump";
    private readonly string speed = "Speed";

    private float targetX;
    private float targetZ;

    [SerializeField] private float xDampTime = 0.1f;
    [SerializeField] private float zDampTime = 0.1f;

    // private int weaponType = 0; // 0 = none, 1 = knife, 2 = secondary, 3 = primary

    void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        playerMotor = GetComponent<PlayerMotor>();
        playerMotor.ProcessMoveEvent += UpdateMovementBlendTree;
    }

    public void UpdateAttackBool(bool attack)
    {
        playerAnimator.SetBool("Attack", attack);
    }

    public void UpdateMovementBlendTree(object sender, PlayerMotor.MoveEventArgs e)
    {
        targetX = e.moveDir.x;
        targetZ = e.moveDir.z;
        playerAnimator.SetFloat(speed, e.currentPlayerSpeed);
    }

    public void UpdateWeaponAnimation(int type)
    {
        Debug.Log("Current weapon type: " + playerAnimator.GetInteger("weaponType"));
        playerAnimator.SetInteger("weaponType", type);
        Debug.Log("Weapon type set to: " + playerAnimator.GetInteger("weaponType"));
    }

    private void Update()
    {
        #region Debugs
        // AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        // if (stateInfo.IsName("2D Blend Tree"))
        // {
        //     Debug.Log("Bledn tree");
        // }
        // else if (stateInfo.IsName("Knife Pull Out"))
        // {
        //     Debug.Log("Knife Pull Out");
        // }
        // else if (stateInfo.IsName("2d Blend Tree 1"))
        // {
        //     Debug.Log("Blend tree 1");
        // }
        // else if (stateInfo.IsName("Knife put in"))
        // {
        //     Debug.Log("Knife put in");
        // }
        #endregion

        //Movement Dampening : 
        playerAnimator.SetFloat(vX, targetX, xDampTime, Time.deltaTime);
        playerAnimator.SetFloat(vZ, targetZ, zDampTime, Time.deltaTime);
    }

    public void Jump()
    {
        playerAnimator.SetTrigger(jump);
    }
}
