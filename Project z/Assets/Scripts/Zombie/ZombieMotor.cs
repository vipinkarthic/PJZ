using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public Transform target;
    public float tick = .1f;
    private NavMeshAgent agent;
    private Vector3 zombieDir;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ProcessMoveEvent?.Invoke(this, new ZombieEventArgs(agent.speed));
    }

    private void Start()
    {
        StartCoroutine(ChasePlayer());
    }

    private IEnumerator ChasePlayer()
    {
        WaitForSeconds wait = new WaitForSeconds(tick);
        while (enabled)
        {
            agent.SetDestination(target.transform.position);
            yield return wait;
        }
    }

    #region  Animation Events
    public event EventHandler<ZombieEventArgs> ProcessMoveEvent;

    public class ZombieEventArgs : EventArgs
    {
        public float speed;
        public ZombieEventArgs(float speed)
        {
            this.speed = speed;
        }
    }
    #endregion
}
