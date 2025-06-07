using UnityEngine;

public class WeaponBareHandsBehaviour : MeleeWeaponBehaviour
{
    private new void Awake()
    {
        base.Awake();
        damage = 5f;
        hitWindow = 0.3f;
    }

    public new void Attack()
    {
        base.Attack();
    }

    protected new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
