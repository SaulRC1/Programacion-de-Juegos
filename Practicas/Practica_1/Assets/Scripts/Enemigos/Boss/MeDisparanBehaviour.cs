using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode(name = "Examples/Me Disparan")]
public class MeDisparanBehaviour : Leaf
{
    [SerializeField] private Animator animator;

    public override NodeResult Execute()
    {
        animator.SetBool("duck", true);
        return NodeResult.success;
    }      
}
