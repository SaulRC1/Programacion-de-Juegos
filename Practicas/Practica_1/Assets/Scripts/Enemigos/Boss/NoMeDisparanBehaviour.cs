using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode(name = "Examples/No Me Disparan")]
public class NoMeDisparanBehavoiur : Leaf
{
    [SerializeField] private Animator animator;

    public override NodeResult Execute()
    {
        animator.SetBool("duck", false);
        //Debug.Log("No Me disparan");
        return NodeResult.success;
    }
}
