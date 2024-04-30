using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] plasmaBalls;

    [Header("Turret timers")]
    [SerializeField] private float damage;
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private float range;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    [Header("Turret sound")]
    [SerializeField] private AudioClip turretSound;
    [SerializeField] private AudioClip plasmaBallSound;

    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private float coolDownTimer = Mathf.Infinity;

    private bool triggered = false;
    private bool active = false;
    private bool load = false;

    private Health player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                load = true;
                StartCoroutine(ActivateTurret());
            }

            player = collision.GetComponent<Health>();
        }
    }*/

    private void Update()
    {
        //Debug.Log("Update");
        if(PlayerInSight())
        {
            //Debug.Log("Detectado");
            if (!triggered)
            {
                load = true;
                StartCoroutine(ActivateTurret());
            }
        }
        
        if (active && player != null && load && coolDownTimer > attackCoolDown)
        {
            Attack();
        }

        coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        GestionSonido.instance.PlaySound(plasmaBallSound);
        coolDownTimer = 0;
        plasmaBalls[FindFireBall()].transform.position = firePoint.position;
        plasmaBalls[FindFireBall()].GetComponent<TurretPlasmaBulletBehaviour>().SetDirection(-1f);
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

    private IEnumerator ActivateTurret()
    {
        //Cambiamos el sprite a rojo para notificarlo al jugador y disparar la trampa
        triggered = true;
        spriteRenderer.color = Color.red;

        GestionSonido.instance.PlaySound(turretSound);
        //Esperamos por el delay de la trampa, activamos la animacion y volvemos a poner el color normal de la trampa
        yield return new WaitForSeconds(activationDelay);
       
        spriteRenderer.color = Color.white;
        active = true;
        animator.SetBool("activated", true);

        //Esperamos X segundos, desactivamos la trampa y volvemos a la animacion original de Idle
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("activated", false);
    }

    private bool PlayerInSight()
    {
        /*RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);*/

        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 
            5f, playerLayer);

        if (hit.collider != null)
        {
            //Debug.Log("Jugador Detectado");
            player = hit.transform.GetComponent<Health>();
        }
        else
        {
            //Debug.Log("Jugador No Detectado");
        }

        return hit.collider != null;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }*/
}
