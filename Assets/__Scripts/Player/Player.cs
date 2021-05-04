using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    // Get the player moving left and right
    // Input.getAxis
    [SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField] private float climbSpeed = 7.0f;
    [SerializeField] private Vector2 death = new Vector2(25f, 25f);

    private bool canDoubleJump = false;
    private bool isGrounded;
    private bool isClimbing;

    private Rigidbody2D rb;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    private Animator myAnimator;
    float gravityScaleAtStart;

    // state of the player
    private bool IsAlive = true; // check in update method


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();// Hitting walls, obstacles and dying
        myFeetCollider = GetComponent<BoxCollider2D>();// Feet collider for climbing and to not jump off walls
        myAnimator = GetComponent<Animator>();
        gravityScaleAtStart = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive == false) return; // Player is dead, no movement is allowed
        PlayerRun();
        PlayerJump();
        FlipSprite(); // Make the sprite flip position
        PlayerClimb();
        PlayerDie(); // check death conditions
    }

    private void PlayerClimb()
    {

        //isClimbing = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false);
            return;
        }

        float vMovement = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rb.velocity.x, vMovement * climbSpeed);
        rb.gravityScale = 0;
        rb.velocity = climbVelocity;
        bool playerHasVSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", playerHasVSpeed); // Reference by string

    }

    private void PlayerRun()
    {
        
        float hMovement = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(hMovement * speed, 0);
        Vector2 runVelocity = new Vector2(hMovement * runSpeed, rb.velocity.y);
        rb.velocity = runVelocity;

        // Set running animation if player has Horizontal speed
        bool playerHasHSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHSpeed); // Reference by string

    }

    private void PlayerJump()
    { 
        // Check if the player is touching the foreground layer
        isGrounded = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"));
                
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;
            }
            else if(canDoubleJump)
            {
                // Half the jump heigh on the second jump
                jumpHeight = jumpHeight / 2;
                Jump();

                canDoubleJump = false;
                // Reset the jump height back to normal
                jumpHeight = jumpHeight * 2;

            }
        }
        
    }

    private void Jump()
    {
        Vector2 jumpVelocityToAdd = new Vector2(0, jumpHeight);
        rb.velocity += jumpVelocityToAdd;
    }

    private void FlipSprite()
    {
        // Detect player pressed arrow
        bool playerHasHorizontalSpeed = Math.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1.0f);
        }
    }

    private void PlayerDie()
    {
        // Is the player layer in contact with enemy layer ? 
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            // player dies
            IsAlive = false;

            myAnimator.SetTrigger("Hurt");

            rb.velocity = death;

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
