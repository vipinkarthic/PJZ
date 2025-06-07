using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponBridge : MonoBehaviour
{
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void StartAttack()
    {
        if (playerController.IsAlive())
        {
            playerController.playerWeaponController.StartHit();
        }
    }

    public void StopAttack()
    {
        if (playerController.IsAlive())
        {
            playerController.playerWeaponController.EndHit();
        }
    }
}
