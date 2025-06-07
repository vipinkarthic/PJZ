using UnityEngine;

public class FollowState : IZombieStates
{
    private readonly ZombieController zombie;

    public FollowState(ZombieController zombie)
    {
        this.zombie = zombie;
    }

    public void EnterState()
    {
        zombie.agent.isStopped = false;
    }

    public void ExecuteState()
    {
        if (!zombie.IsPlayerInDetectionRadius())
        {
            zombie.ChangeState(zombie.patrolState);
        }
        else if (zombie.IsPlayerInAttackRadius())
        {
            zombie.ChangeState(zombie.attackState);
        }
        else
        {
            zombie.agent.SetDestination(zombie.player.transform.position);
            zombie.agent.speed = zombie.zombieSpeed;
        }
    }

    public void ExitState()
    {
        // uhm later
        zombie.agent.ResetPath(); //idk just testing
    }
}
