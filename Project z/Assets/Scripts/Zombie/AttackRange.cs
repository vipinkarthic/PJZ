using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public IDamageable player;
    public float attackDelay = 0.5f;
    public delegate void AttackEvent(IDamageable target);
    public AttackEvent OnAttackEvent;
    private Coroutine AttackCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            player = damageable;
            if (AttackCoroutine == null)
            {
                AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            player = null;
            if (player != null && AttackCoroutine != null)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
            }
        }
    }

    private IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        yield return wait;

        if (player != null)
        {
            OnAttackEvent?.Invoke(player);
            player.TakeDamage(10f);
        }


    }

}
