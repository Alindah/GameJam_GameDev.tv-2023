using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private float ringDuration = 2.0f;
    [SerializeField] private float restDuration = 2.0f;
    private Animator anim;
    private bool isRinging = false;

    private const string ANIM_REST = "clock_idle";
    private const string ANIM_RING = "clock_ring";

    private void Start()
    {
        anim = GetComponent<Animator>();
        Invoke(nameof(Ring), 0.0f);
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
}
