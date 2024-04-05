using System;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float velocidad;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * velocidad, body.velocity.y);

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }

        //Set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, velocidad);
        animator.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }
}
