using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour, IDamageable
{
    private IZombieStates currentState;

    // Stat - Only Health for now
    [HideInInspector] public ZombieStat healthStat;

    [SerializeField] public PlayerController player;
    [SerializeField] public float patrolRadius;
    [SerializeField] public float detectionRadius;
    [SerializeField] public float attackRadius;
    [SerializeField] public float attackDamage;
    [SerializeField] public float attackDelay;
    [SerializeField] public float zombieSpeed;

    // AI
    [HideInInspector] public NavMeshAgent agent;

    // FSM
    public PatrolState patrolState;
    public FollowState followState;
    public AttackState attackState;
    public IdleState idleState;

    public Transform GetTransform()
    {
        return transform;
    }

    public void ChangeState(IZombieStates newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        agent.updateRotation = false;

        patrolState = new PatrolState(this);
        followState = new FollowState(this);
        attackState = new AttackState(this);
        idleState = new IdleState(this);

        healthStat = new();
        healthStat.Initialize(100f);
    }

    private void Start()
    {
        currentState = patrolState;
        currentState.EnterState();
    }

    private void Update()
    {
        currentState.ExecuteState();
        ProcessMoveEvent?.Invoke(this, new ZombieEventArgs(agent.speed));
        HandleRotation();
    }

    public void AttackPlayer()
    {
        if (player.IsAlive())
        {
            player.playerStatsManager.TakeDamage(attackDamage);
        }
    }

    public void HandleRotation()
    {
        Vector3 velocity = agent.velocity;
        velocity.y = 0;
        if (velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    #region Helper Methods
    public bool IsPlayerInDetectionRadius()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= detectionRadius;
    }

    public bool IsPlayerInAttackRadius()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= attackRadius;
    }
    #endregion


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

    public void TakeDamage(float damage)
    {
        healthStat.SubtractValue(damage);
        Debug.Log($"Zombie took {damage} damage. HP reduced from {healthStat.currentValue + damage} to {healthStat.currentValue}");
        if (healthStat.currentValue <= 0)
        {
            // Handle zombie death
            Destroy(gameObject);
        }
    }

}