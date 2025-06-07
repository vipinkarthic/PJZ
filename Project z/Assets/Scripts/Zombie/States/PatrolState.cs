using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IZombieStates
{
    private readonly ZombieController zombie;
    private Vector3 targetPoint;

    public PatrolState(ZombieController zombie)
    {
        this.zombie = zombie;
    }

    public void EnterState()
    {
        zombie.agent.isStopped = false;
        targetPoint = GetRandomNavmeshPoint(zombie.transform.position, zombie.patrolRadius);
        zombie.agent.SetDestination(targetPoint);
    }

    public void ExecuteState()
    {
        if (zombie.IsPlayerInDetectionRadius())
        {
            zombie.ChangeState(zombie.followState);
        }
        else if (Vector3.Distance(zombie.transform.position, targetPoint) < 1f)
        {
            targetPoint = GetRandomNavmeshPoint(zombie.transform.position, zombie.patrolRadius);
            zombie.agent.SetDestination(targetPoint);
        }
    }

    public void ExitState()
    {
        // uhm later
    }

    private Vector3 GetRandomNavmeshPoint(Vector3 origin, float radius)
    {
        Vector3 randomPoint = Random.insideUnitSphere * radius + origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }
}