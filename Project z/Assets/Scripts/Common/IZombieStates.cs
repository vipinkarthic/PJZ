using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombieStates
{
    void EnterState(); //setup when entering a state
    void ExitState(); //cleanup when exiting a state
    void ExecuteState(); //What to do when in this state
}