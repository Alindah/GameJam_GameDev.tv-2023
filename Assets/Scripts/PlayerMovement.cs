using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;


    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool hasDoubleJumped = false;
    private Animator anim;
    
    private const string ANIM_IDLE = "idle";
    private const string ANIM_JUMP = "jump";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cloud"))
        {
            isGrounded = true;
            hasDoubleJumped = false;
            anim.Play(ANIM_IDLE);
        }
    }

    /// <summary>
    /// Moves the player according to user input.
    /// </summary>
    private void MovePlayer()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (!hasDoubleJumped)
            {
                Jump();
                hasDoubleJumped = true;
            }
        }

        // Horizontal movement
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            FlipPlayer(1);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            FlipPlayer(-1);
        }
        else
        {
            transform.Translate(Vector3.zero);
        }
    }

    /// <summary>
    /// Flips the player to face left or right.
    /// </summary>
    /// <param name="direction">1 for default direction, -1 to face opposite direction.</param>
    private void FlipPlayer(float direction)
    {
        transform.localScale = new Vector3(direction * Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        anim.Play(ANIM_JUMP);
    }
}
