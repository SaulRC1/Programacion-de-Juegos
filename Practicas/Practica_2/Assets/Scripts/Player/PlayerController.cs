using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    [SerializeField] 
    private float jumpForce;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private float groundDistance;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private bool isCharacterGrounded = false;

    private Vector3 velocity = Vector3.zero;

    private CharacterController characterController;

    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        moveSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        handleIsGrounded();

        handleJumping();

        handleGravity();

        handleRunning();
        handleMovement();
    }

    private void getReferences()
    {

    }

    private void handleJumping()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    private void handleGravity()
    {
        if (isCharacterGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void handleIsGrounded()
    {
        isCharacterGrounded = Physics.CheckSphere(transform.position, groundDistance);
    }

    private void handleRunning()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            moveSpeed = runSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
        }
    }

    private void handleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = moveDirection.normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
