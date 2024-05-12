using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 3.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        agent.SetDestination(player.position);       
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > 15)
        {
            animator.SetBool("isRunning", false);
        }
        if (distance < 2.5f)
        {
            animator.SetBool("isAttacking", true);
        }

        /*float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > 15)
        {
            animator.SetBool("isRunning", false);
        }
        else if (distance < 2.5f)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            // Actualiza el destino solo si el agente aún no ha alcanzado la posición del jugador
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                // Detén el movimiento y la rotación del agente si ya está cerca del jugador
                agent.velocity = Vector3.zero;
                agent.angularSpeed = 0f;
            }
        }*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
