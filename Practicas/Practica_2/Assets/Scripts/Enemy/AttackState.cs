using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    Transform player;

    private DateTime lastTimePlayerHit;
    private int playerHitDelayInSeconds = 1;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > 3.5f)
        {
            animator.SetBool("isAttacking", false);
        }
        else
        {
            //Ray ray = animator.transform.GameObject.//camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            float enemyRange = 2f;

            if (Physics.Raycast(animator.transform.position, animator.transform.TransformDirection(Vector3.forward),
                out hit, enemyRange/*, player.gameObject.layer*/))
            {
                GameObject hitGameObject = hit.transform.gameObject;

                if (hitGameObject != null)
                {
                    //Debug.Log("Hit: " +  hitGameObject.name);
                    PlayerController playerController = hitGameObject.GetComponent<PlayerController>();

                    if (playerController != null && lastTimePlayerHit.AddSeconds(playerHitDelayInSeconds).CompareTo(DateTime.Now) <= 0)
                    {
                        //Debug.Log("Player hit");
                        playerController.characterStatistics.TakeDamage(animator.transform.gameObject.GetComponent<EnemyCore>().damageGene);
                        lastTimePlayerHit = DateTime.Now;
                    }
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
