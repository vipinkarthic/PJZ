using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerWeaponManager playerWeaponManager;
    private IWeaponBehaviour currentWeaponBehaviour;
    private void Awake()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
        playerWeaponManager.OnWeaponEquipped += newWeaponBehaviour => currentWeaponBehaviour = newWeaponBehaviour;
    }

    //Attak called here on input
    public void Attack()
    {
        currentWeaponBehaviour?.Attack();
    }

    public void StartHit()
    {
        if (currentWeaponBehaviour is MeleeWeaponBehaviour meleeWeapon)
        {
            meleeWeapon.StartHit();
        }
    }

    public void EndHit()
    {
        if (currentWeaponBehaviour is MeleeWeaponBehaviour meleeWeapon)
        {
            meleeWeapon.EndHit();
        }
    }

}

//     private void Update()
//     {
//         if (Input.GetMouseButton(0))
//         {
//             if (playerWeaponManager.IsWeaponEquipped() != -1)
//             {
//                 //for now im doing the sword logic here, but in the future it will be in its own weapon controller script
//                 //this is where the sword will be swung
//                 if (canAttack)
//                 {
//                     SwordAttack();
//                 }
//             }
//         }
//     }

//     private void SwordAttack()
//     {
//         canAttack = false;
//         playerAnimation.UpdateAttackBool(true);
//         StartCoroutine(ResetAttack());
//     }

//     private IEnumerator ResetAttack()
//     {
//         yield return new WaitForSeconds(attackCooldown);
//         playerAnimation.UpdateAttackBool(false);
//         canAttack = true;
//     }
// }
