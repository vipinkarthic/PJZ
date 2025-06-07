using UnityEngine;

public class WeaponKnifeBehaviour : MeleeWeaponBehaviour
{
    [SerializeField]
    private new void Awake()
    {
        base.Awake();
        damage = 15f;
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