using Assets.Scripts.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, ICharacterStatusListener
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

    public CharacterStatistics characterStatistics;

    [Header("Character Statistics")]
    [SerializeField] private Image[] healthBar;
    private int nextHealthBarDamageIndex = 7;
    private int nextHealthBarHealIndex = 7;

    private DateTime lastTimePlayerHealed = DateTime.Now;
    private int playerHealDelayInSeconds = 10;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        moveSpeed = walkSpeed;

        characterStatistics = new CharacterStatistics(8, 8, false);
        characterStatistics.AddCharacterStatusListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        handleIsGrounded();

        handleJumping();

        handleGravity();

        handleRunning();
        handleMovement();

        if(lastTimePlayerHealed.AddSeconds(playerHealDelayInSeconds).CompareTo(DateTime.Now) <= 0)
        {
            characterStatistics.Heal(1);
            lastTimePlayerHealed = DateTime.Now;
        }
    }

    private void getReferences()
    {

    }

    private void handleJumping()
    {
        if (Input.GetKey(KeyCode.Space) && isCharacterGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
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
        isCharacterGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
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

    public void OnCharacterDeath()
    {
        SceneManager.LoadScene("Game Over Screen");
        PlayerScore.resetScore();
    }

    public void onCharacterHealed()
    {
        if (nextHealthBarHealIndex >= 0 && nextHealthBarHealIndex <= 7)
        {
            //Debug.Log("Healed");
            Image healthTank = healthBar[nextHealthBarHealIndex];

            healthTank.gameObject.SetActive(true);

            if(nextHealthBarHealIndex < 7)
            {
                nextHealthBarDamageIndex = nextHealthBarHealIndex;
                nextHealthBarHealIndex++;
            }
            else
            {
                nextHealthBarDamageIndex = 7;
                nextHealthBarHealIndex = 7;
            }
        }
    }

    public void onCharacterDamaged()
    {
        if(nextHealthBarDamageIndex >= 0 && nextHealthBarDamageIndex <= 7)
        {
            //Debug.Log("Damaged");
            Image healthTank = healthBar[nextHealthBarDamageIndex];

            healthTank.gameObject.SetActive(false);

            if(nextHealthBarDamageIndex > 0) 
            {
                nextHealthBarHealIndex = nextHealthBarDamageIndex;
                nextHealthBarDamageIndex--;
            }
            else
            {
                nextHealthBarHealIndex = 0;
                nextHealthBarDamageIndex = 0;
            }
        }
    }
}
