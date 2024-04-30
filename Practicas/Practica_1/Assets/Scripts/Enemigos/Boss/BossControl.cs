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
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] plasmaBalls;
    [SerializeField] private float attackCoolDown;

    private System.Random rnd = new System.Random();
    private float coolDownTimer = Mathf.Infinity;

    void Reset()
    {
        monoBehaviourTree = GetComponent<MonoBehaviourTree>();
        OnValidate();
    }

    void Update()
    {
        playerShooting();
        shootPlayer();    
        monoBehaviourTree.Tick();
        coolDownTimer += Time.deltaTime;
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
                meDisparan.Value = true;
                noMeDisparan.Value = false;
            }         
        }
        else
        {
            meDisparan.Value = false;
            noMeDisparan.Value = true;
        }
    }

    private void shootPlayer()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left,
            9f, playerLayer);

        BoolVariable veoJugador = blackboard.GetVariable<BoolVariable>("veoJugador");

        int num = rnd.Next(1, 11);

        if (num == 1)
        {
            if (hit.collider != null)
            {
                if (coolDownTimer > attackCoolDown)
                {
                    Attack();
                    veoJugador.Value = true;
                }            
            }
        }
        else
        {
            veoJugador.Value = false;
        }
    }

    private void Attack()
    {
        //GestionSonido.instance.PlaySound(plasmaBallSound);
        coolDownTimer = 0;
        plasmaBalls[FindFireBall()].transform.position = firePoint.position;
        plasmaBalls[FindFireBall()].GetComponent<TurretPlasmaBulletBehaviour>().SetDirection(-61f);
    }

    private int FindFireBall()
    {
        for (int i = 0; i < plasmaBalls.Length; i++)
        {
            if (!plasmaBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
