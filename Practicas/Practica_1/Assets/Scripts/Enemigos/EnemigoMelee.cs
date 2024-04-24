using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemigoMelee : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;  
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Sword sound")]
    [SerializeField] private AudioClip swordAttack;
         
    private Animator animator;
    private Health playerHealt;
    private EnemigoPatrulla enemigoPatrulla;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemigoPatrulla = GetComponentInParent<EnemigoPatrulla>();
        
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        animator.SetBool("run", true);
        //Attack only when player in sight
        if (PlayerInSight())
        {
            animator.SetBool("run", false);
            if (cooldownTimer >= attackCooldown && playerHealt.currentHealth > 0)
            {
                cooldownTimer = 0;
                animator.SetTrigger("attack");
                GestionSonido.instance.PlaySound(swordAttack);
            }
        }    
        
        if (enemigoPatrulla != null)
        {
            enemigoPatrulla.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealt = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealt.TakeDamage(damage);
        }
    }
}
