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
    public SoundEffectHandler SoundEffectHandler;

    private Vector3 ref_velocity = Vector3.zero;
    public Vector3 touchCoords;
    public Vector3 firstTouchCoords;
    public Vector3 lastTouchCoords;
    public List<KeyValuePair<float, float>> touchHistory = new List<KeyValuePair<float, float>>(); // <time, y pos>
    public bool clickJump = false;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool touchIsGoingJump = false;

    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapArea(PLeft.position, PRight.position) && rb.velocity.y == 0;
        if ((Input.GetButtonDown("Jump") || touchIsGoingJump) && isGrounded)
        {
            clickJump = true;
            touchIsGoingJump = false;
            SoundEffectHandler.playSound("jump");
        }
        if (isJumping && isGrounded)
        {
            isJumping = false;
        }
        animator.SetBool("isJumping", isJumping);
    }

    void FixedUpdate()
    {
        getTouchCoords();
        float direction = Input.GetAxisRaw("Horizontal");
        if (touchCoords != Vector3.zero && Mathf.Abs(firstTouchCoords.x - touchCoords.x) > 0.2)
        {
            direction = (firstTouchCoords.x < touchCoords.x) ? 1 : -1;
        }
        float horizontalMovement = direction * moveSpeed * Time.deltaTime;

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
            Vector3 turned_pos = player.gameObject.transform.localScale;
            turned_pos.x = Mathf.Abs(turned_pos.x);
            player.gameObject.transform.localScale = turned_pos;
        }
        if (velocity < -0.1f)
        {
            Vector3 turned_pos = player.gameObject.transform.localScale;
            turned_pos.x = -Mathf.Abs(turned_pos.x);
            player.gameObject.transform.localScale = turned_pos;
            
        }
    }

    void getTouchCoords()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchCoords = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began || Mathf.Abs(firstTouchCoords.x - touchCoords.x) < Mathf.Abs(firstTouchCoords.x - lastTouchCoords.x))
                firstTouchCoords = touchCoords;
            touchHistory.Add(new KeyValuePair<float, float>(Time.time, touchCoords.y));
            lastTouchCoords = touchCoords;
            touchIsGoingJump = checkTouchIsJumping();
        }
        else
        {
            touchCoords = Vector3.zero;
            firstTouchCoords = Vector3.zero;
            touchIsGoingJump = false;
            touchHistory.Clear();
        }
    }

    bool checkTouchIsJumping()
    {
        bool ret = false;
        KeyValuePair<float, float> pairToRemove = default(KeyValuePair<float, float>);
        do
        {
            if (!pairToRemove.Equals(default(KeyValuePair<float, float>)))
            {
                touchHistory.Remove(pairToRemove);
                pairToRemove = default(KeyValuePair<float, float>);
            }
            foreach (KeyValuePair<float, float> pair in touchHistory)
            {
                if (touchCoords.y - pair.Value > 0.15)
                {
                    ret = true;
                }
                if (pair.Key < Time.time - 1)
                {
                    pairToRemove = pair;
                }
                else
                {
                    pairToRemove = default(KeyValuePair<float, float>);
                }
            }
            if (ret == true)
            {
                touchHistory.Clear();
                return true;
            }
        } while (!pairToRemove.Equals(default(KeyValuePair<float, float>)));
        return ret;
    }
}
