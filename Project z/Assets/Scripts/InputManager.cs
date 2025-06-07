using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputSystem playerInput;
    private PlayerInputSystem.PlayerActions playerActions;
    private PlayerInputSystem.WeaponsActions weaponActions;
    private PlayerInputSystem.MeleeActions meleeActions;
    private PlayerController player;

    // Start is called before the first frame update

    void Awake()
    {
        playerInput = new PlayerInputSystem();
        playerActions = playerInput.Player;
        weaponActions = playerInput.Weapons;
        meleeActions = playerInput.Melee;

        player = GetComponentInChildren<PlayerController>();

        // Bind input actions to player motor methods
        playerActions.Jump.performed += ctx => player.playerMotor.Jump();
        playerActions.Crouch.performed += ctx => player.playerMotor.Crouch();
        playerActions.Sprint.performed += ctx => player.playerMotor.Sprint();

        //Bind input actions to player weapon methods
        weaponActions.EquipKnife.performed += ctx => player.playerWeaponManager.Equip(WeaponType.Knife);
        weaponActions.EquipPistol.performed += ctx => player.playerWeaponManager.Equip(WeaponType.Pistol);
        weaponActions.EquipRifle.performed += ctx => player.playerWeaponManager.Equip(WeaponType.Rifle);
        weaponActions.UnequipAnything.performed += ctx => player.playerWeaponManager.Unequip();
        meleeActions.MainAttack.performed += ctx => player.playerWeaponController.Attack();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        player.playerMotor.ProcessMove(playerActions.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        player.playerLook.ProcessLook(playerActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerActions.Enable();
        weaponActions.Enable();
        meleeActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
        weaponActions.Disable();
        meleeActions.Disable();
    }
}
