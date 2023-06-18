using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private float ringDuration = 2.0f;
    [SerializeField] private float restDuration = 2.0f;
    [SerializeField] private float gravityScale = 3.0f;

    private Animator anim;
    private Rigidbody2D rb;
    private GameObject player;
    private bool isRinging = false;
    private bool playerIsTouching = false;

    private const string ANIM_REST = "clock_idle";
    private const string ANIM_RING = "clock_ring";
    private const string PLAYER_TAG = "Player";
    private const string PLAYER_OBJ_NAME = "Player";

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find(PLAYER_OBJ_NAME);
        Invoke(nameof(Ring), 0.0f);
    }

    private void Update()
    {
        AnalyzePlayerContact();
    }

    private void Ring()
    {
        isRinging = true;
        anim.Play(ANIM_RING);
        Invoke(nameof(Rest), ringDuration);
    }

    private void Rest()
    {
        isRinging = false;
        anim.Play(ANIM_REST);
        Invoke(nameof(Ring), restDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
            playerIsTouching = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
            playerIsTouching = false;

    }

    // Determine if player is turning off clock
    private void AnalyzePlayerContact()
    {
        if (playerIsTouching && (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Vertical") > 0))
        {
            // Player may only disable a clock if landing on it
            if (player.GetComponent<Rigidbody2D>().velocityY < 0)
                rb.gravityScale = gravityScale;
        }
    }
}
