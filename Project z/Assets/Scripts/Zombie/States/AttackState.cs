using UnityEngine;

public class AttackState : IZombieStates
{
    private readonly ZombieController zombie;
    private float attackCooldown;

    public AttackState(ZombieController zombie)
    {
        this.zombie = zombie;
        attackCooldown = 0f;
    }

    public void EnterState()
    {
        zombie.agent.isStopped = true;
        zombie.agent.speed = 0f;
        zombie.agent.ResetPath();
        attackCooldown = 0f;
    }

    public void ExecuteState()
    {
        if (!zombie.IsPlayerInAttackRadius())
        {
            if (zombie.IsPlayerInDetectionRadius())
            {
                zombie.ChangeState(zombie.followState);
            }
            else
            {
                zombie.ChangeState(zombie.patrolState);
            }
        }
        else
        {
            attackCooldown += Time.deltaTime;
            if (attackCooldown >= zombie.attackDelay)
            {
                zombie.AttackPlayer();
                attackCooldown = 0f;
            }
        }
    }

    public void ExitState()
    {
        zombie.agent.isStopped = false;
    }
}
