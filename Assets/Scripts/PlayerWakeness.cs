using UnityEngine;
using UnityEngine.UI;

public class PlayerWakeness : MonoBehaviour
{
    [Tooltip("Determines how awake player needs to be until game over")]
    [SerializeField] private float wakenessCapacity = 100.0f;
    [Tooltip("Rate at which player becomes sleepy again")]
    [SerializeField] private float recoverRate = 10.0f;
    [Tooltip("Delay before player wakeness drains")]
    [SerializeField] private float recoverDelay = 1.5f;

    [SerializeField] private GameStateHandler gameStateHandler;
    [SerializeField] private Slider wakeSlider;

    private float wakeness = 0.0f;
    private bool canRecover = true;

    private void Start()
    {
        wakeSlider.maxValue = wakenessCapacity;
    }

    private void Update()
    {
        Recover();
    }

    public void AddWakeness(float amount)
    {
        if (IsInvoking(nameof(EnableRecover)))
            CancelInvoke(nameof(EnableRecover));

        canRecover = false;
        wakeness += amount * Time.deltaTime;
        wakeSlider.value = wakeness;

        if (wakeness >= wakenessCapacity)
            gameStateHandler.LoseGame();

        Invoke(nameof(EnableRecover), recoverDelay);
    }

    private void EnableRecover()
    {
        canRecover = true;
    }

    private void Recover()
    {
        if (canRecover)
        {
            wakeness = wakeness > 0.0f ? wakeness - recoverRate * Time.deltaTime : 0.0f;
            wakeSlider.value = wakeness;
        }
    }
}
