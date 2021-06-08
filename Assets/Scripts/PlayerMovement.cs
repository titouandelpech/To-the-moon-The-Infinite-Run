using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public SpriteRenderer player;
    public Rigidbody2D rb;
    public Transform PLeft;
    public Transform PRight;
    public Animator animator;

    private Vector3 ref_velocity = Vector3.zero;
    public bool clickJump = false;
    public bool isGrounded = false;
    public bool isJumping = false;

    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapArea(PLeft.position, PRight.position);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            clickJump = true;
        }
        if (isJumping && isGrounded)
        {
            isJumping = false;
        }
        animator.SetBool("isJumping", isJumping);
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;

        MovePlayer(horizontalMovement);
        CheckFlip(horizontalMovement);
        animator.SetFloat("velocity", Mathf.Abs(horizontalMovement));
    }

    void MovePlayer(float horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref ref_velocity, .05f);
        if (clickJump)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(0f, jumpForce));
            clickJump = false;
            isJumping = true;
        }
    }

    void CheckFlip(float velocity)
    {
        if (velocity > 0.1f)
        {
            player.flipX = false;
        }
        if (velocity < -0.1f)
        {
            player.flipX = true;
        }
    }
}
