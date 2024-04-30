using MBT;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MonoBehaviourTree))]
public class BossControl : MonoBehaviour
{

    public MonoBehaviourTree monoBehaviourTree;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Blackboard blackboard;
    [SerializeField] private LayerMask proyectilesLayer;
    [SerializeField] private Animator animator;
    private System.Random rnd = new System.Random();

    void Reset()
    {
        monoBehaviourTree = GetComponent<MonoBehaviourTree>();
        OnValidate();
    }

    void Update()
    {
        playerShooting();
        monoBehaviourTree.Tick();
    }

    void OnValidate()
    {
        if (monoBehaviourTree != null && monoBehaviourTree.parent != null)
        {
            monoBehaviourTree = null;
            Debug.LogWarning("Subtree should not be target of update. Select parent tree instead.", this.gameObject);
        }
    }

    private void playerShooting()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left,
            1f, proyectilesLayer);

        BoolVariable meDisparan = blackboard.GetVariable<BoolVariable>("meDisparan");
        BoolVariable noMeDisparan = blackboard.GetVariable<BoolVariable>("noMeDisparan");

        int num = rnd.Next(1, 11);

        if (num == 1 )
        {
            if (hit.collider != null)
            {
                //Debug.Log("me disparan a true");
                meDisparan.Value = true;
                noMeDisparan.Value = false;
            }         
        }
        else
        {
            //Debug.Log("me disparan a false");
            meDisparan.Value = false;
            noMeDisparan.Value = true;
        }
    }
}
