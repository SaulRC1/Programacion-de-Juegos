using System.Collections;
using UnityEngine;

public class LaserTower : MonoBehaviour
{
    [Header("LaserTower Timers")]
    [SerializeField] private float damage;
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool triggered; // Cuando la trampa se dispare
    private bool active; // Cuando al trampa se active y pueda dañar al jugador
    private bool load;
    private Health player;

    [Header("Laser sound")]
    [SerializeField] private AudioClip laserSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                load = true;
                StartCoroutine(ActivateElectricTower());
            }

            player = collision.GetComponent<Health>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }

    private void Update()
    {
        if (active && player != null && load)
        {
            player.TakeDamage(damage);
            load = false;
        }
    }

    private IEnumerator ActivateElectricTower()
    {
        //Cambiamos el sprite a rojo para notificarlo al jugador y disparar la trampa
        triggered = true;
        spriteRenderer.color = Color.red;

        //Esperamos por el delay de la trampa, activamos la animacion y volvemos a poner el color normal de la trampa
        yield return new WaitForSeconds(activationDelay);
        GestionSonido.instance.PlaySound(laserSound);
        spriteRenderer.color = Color.white;
        active = true;
        animator.SetBool("activated", true);

        //Esperamos X segundos, desactivamos la trampa y volvemos a la animacion original de Idle
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("activated", false);
    }
}
