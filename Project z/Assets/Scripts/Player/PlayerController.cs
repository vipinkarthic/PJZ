using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Transform location;

    //References to everything
    [HideInInspector] public PlayerLook playerLook;
    [HideInInspector] public PlayerStatsManager playerStatsManager;
    [HideInInspector] public PlayerWeaponManager playerWeaponManager;
    [HideInInspector] public PlayerWeaponController playerWeaponController;
    [HideInInspector] public PlayerMotor playerMotor;

    private void Awake()
    {
        location = transform;
        playerLook = GetComponent<PlayerLook>();
        playerStatsManager = GetComponentInChildren<PlayerStatsManager>();
        playerWeaponManager = GetComponentInChildren<PlayerWeaponManager>();
        playerWeaponController = GetComponentInChildren<PlayerWeaponController>();
        playerMotor = GetComponent<PlayerMotor>();
    }

    public bool IsAlive()
    {
        if (playerStatsManager.GetHealthStat() <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}