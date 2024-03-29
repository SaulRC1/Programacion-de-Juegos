using System;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;
    [SerializeField] private float velocidad;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * velocidad, body.velocity.y);

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        //Set animator parameters
        animator.SetBool("run", horizontalInput != 0);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, velocidad);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
