using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5.0f;
    [Tooltip("How high the player jumps")]
    [SerializeField] private float jumpForce = 5.0f;
    [Tooltip("How long to disable collider for cloud after player drops down")]
    [SerializeField] private float dropSeconds = 0.4f;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool hasDoubleJumped = false;
    // private bool hasDropped = false;
    private Animator anim;
    
    private const string ANIM_IDLE = "idle";
    private const string ANIM_JUMP = "jump";

    private const string LAYER_DEFAULT = "Default";
    private const string LAYER_PLAYERJUMP = "PlayerJumping";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();

        if (rb.velocityY > 0)
            gameObject.layer = LayerMask.NameToLayer(LAYER_PLAYERJUMP);
        else
            gameObject.layer = LayerMask.NameToLayer(LAYER_DEFAULT);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cloud") && rb.velocityY <= 0)
        {
            hasDoubleJumped = false;
            isGrounded = true;
            rb.velocity = Vector2.zero;
            anim.Play(ANIM_IDLE);
        }
    }

    /// <summary>
    /// Moves the player according to user input.
    /// </summary>
    private void MovePlayer()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Vertical") > 0)
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

        /*
        // Vertical movement
        if (Input.GetAxis("Vertical") < 0 && isGrounded)
        {
            hasDropped = true;
            gameObject.layer = LayerMask.NameToLayer(LAYER_PLAYERJUMP);
            StartCoroutine(nameof(DropDown));
        }
        */
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
        anim.Play(ANIM_JUMP);
        isGrounded = false;
    }

    /*
    private IEnumerator DropDown()
    {
        yield return new WaitForSeconds(dropSeconds);
        gameObject.layer = LayerMask.NameToLayer(LAYER_DEFAULT);
        hasDropped = false;
    }
    */
}
