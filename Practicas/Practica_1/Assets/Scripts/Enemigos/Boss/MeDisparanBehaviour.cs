using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode(name = "Examples/Me Disparan")]
public class MeDisparanBehaviour : Leaf
{

    public override NodeResult Execute()
    {
        Debug.Log("Me disparan");
        return NodeResult.success;
    }      
}
