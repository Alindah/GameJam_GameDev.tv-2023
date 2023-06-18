using UnityEngine;

public class PlayerWakeness : MonoBehaviour
{
    [Tooltip("Determines how awake player needs to be until game over")]
    [SerializeField] private float wakenessCapacity = 100.0f;
    
    private float wakeness = 0f;

    public void AddWakeness(float amount)
    {
        wakeness += amount;
        Debug.Log("Wakeness: " + wakeness);

        if (wakeness >= wakenessCapacity)
            Debug.Log("Game over!");
    }
}
