using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] plasmaBalls;

    private Animator animator;
    private MovimientoJugador movimientoJugador;
    private float coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movimientoJugador = GetComponent<MovimientoJugador>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer > attackCoolDown && movimientoJugador.canAttack())
        {
            Attack();
        }

        coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        coolDownTimer = 0;
        plasmaBalls[FindFireBall()].transform.position = firePoint.position;
        plasmaBalls[FindFireBall()].GetComponent<Proyectil>().SetDirection(Mathf.Sign(transform.localScale.x));
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
