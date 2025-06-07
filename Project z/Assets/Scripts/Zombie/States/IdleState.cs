using UnityEngine;

public class IdleState : IZombieStates
{
    private ZombieController zombie;

    public IdleState(ZombieController zombie)
    {
        this.zombie = zombie;
    }

    public void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public void ExecuteState()
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }
}

