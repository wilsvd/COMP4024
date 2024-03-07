using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The BossRun class defines the behavior of the boss while in the "Run" state.
public class BossRun : StateMachineBehaviour
{
    public float speed = 2.5f;

    // Reference to the player's Transform.
    Transform player;

    // Reference to the Rigidbody2D component of the boss.
    Rigidbody2D rb;

    // Reference to the Boss script attached to the boss.
    Boss boss;

    // The range at which the boss initiates an attack.
    public float attackRange = 3f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Make the boss face the player's direction.
        boss.LookAtPlayer(player);

        // Calculate the target position to move towards (same y-coordinate as the boss).
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // Check if the player is within the attack range, trigger the "Attack" animation.
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset the "Attack" trigger when exiting the "Run" state.
        animator.ResetTrigger("Attack");
    }

}
