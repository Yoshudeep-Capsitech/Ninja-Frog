using UnityEngine;

public class TrapTriggerSpikes : MonoBehaviour
{
    [SerializeField] private OscillatingTrap[] spikesToActivate;

    private bool hasBeenTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenTriggered && other.CompareTag("Player"))
        {
            hasBeenTriggered = true;

            foreach (OscillatingTrap spike in spikesToActivate)
            {
                spike.ActivateTrap();
            }
        }
    }
}