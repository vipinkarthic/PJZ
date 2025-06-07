using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private Animator zombieAnimator;
    private ZombieController zombie;

    private void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        zombie = GetComponent<ZombieController>();

        zombie.ProcessMoveEvent += UpdateMovementBlendTree;
    }

    private void UpdateMovementBlendTree(object sender, ZombieController.ZombieEventArgs e)
    {
        zombieAnimator.SetFloat("Velocity X", e.speed);
    }
}
