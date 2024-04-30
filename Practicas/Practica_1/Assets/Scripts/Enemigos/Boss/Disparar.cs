using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode(name = "Examples/Disparar")]
public class Disparar : Leaf
{
    [SerializeField] private Animator animator;

    public override NodeResult Execute()
    {
        animator.SetTrigger("attack");
        return NodeResult.success;
    }
}
