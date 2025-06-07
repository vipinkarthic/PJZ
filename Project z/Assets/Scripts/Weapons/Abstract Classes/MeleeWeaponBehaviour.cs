using System.Collections;
using UnityEngine;
public abstract class MeleeWeaponBehaviour : MonoBehaviour, IWeaponBehaviour
{
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float hitWindow = .5f;

    private BoxCollider weaponCollider;
    private PlayerAnimation playerAnimation;
    protected bool canAttack = true;

    public void Awake()
    {
        weaponCollider = GetComponent<BoxCollider>();
        playerAnimation = GetComponentInParent<PlayerAnimation>();

        CharacterController playerCharacterController = GetComponentInParent<CharacterController>();
        if (playerCharacterController != null)
        {
            Physics.IgnoreCollision(weaponCollider, playerCharacterController, true);
        }
        weaponCollider.enabled = false;
    }

    public void Attack()
    {
        // StartCoroutine(PerformAttack()); NOW IT IS SET BASED ON ANIMATION EVENTS
        playerAnimation.UpdateAttackBool(true);
    }

    public void StartHit()
    {
        canAttack = true;
        weaponCollider.enabled = true;
    }

    // Called from Animation Event “EndHit”
    public void EndHit()
    {
        canAttack = false;
        weaponCollider.enabled = false;
        playerAnimation.UpdateAttackBool(false);
    }

    // private IEnumerator PerformAttack()
    // {
    //     if (canAttack)
    //     {
    //         canAttack = false;
    //         weaponCollider.enabled = true;
    //         yield return new WaitForSeconds(hitWindow);
    //         weaponCollider.enabled = false;
    //         canAttack = true;
    //         playerAnimation.UpdateAttackBool(false);
    //     }
    // }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon hit: " + other.name);
        if (true)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    damageable.TakeDamage(damage);
                    canAttack = false;
                    weaponCollider.enabled = false;
                }

            }

        }

    }
}

