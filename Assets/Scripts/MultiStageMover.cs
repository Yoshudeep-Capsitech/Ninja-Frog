using UnityEngine;
using System.Collections;

public class MultiStageMover : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The object that will move (e.g., Pillar_4 or LeftPlatform).")]
    [SerializeField] private GameObject objectToMove;

    [Tooltip("The sequence of movements. Each step is an OFFSET from the last position.")]
    [SerializeField] private Vector3[] moveSteps = new Vector3[1]; // Default to 1 step

    [Tooltip("How long each individual movement step takes (in seconds).")]
    [SerializeField] private float durationPerStep = 1.0f;

    [Tooltip("Time to wait between completing one step and starting the next.")]
    [SerializeField] private float delayBetweenSteps = 0.2f;

    [Tooltip("The sound to play ONCE when the whole sequence starts.")]
    [SerializeField] private AudioClip moveSound;

    [Header("Chaining")]
    [Tooltip("(Optional) The *next* mover to trigger after this one finishes.")]
    [SerializeField] private MultiStageMover nextMoverToTrigger;

    [Tooltip("If true, this mover will NOT start automatically when triggered. It must be started by a 'nextMoverToTrigger' chain.")]
    [SerializeField] private bool requiresChainedStart = false;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (requiresChainedStart)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            StartMoving();
        }
    }
    public void StartMoving()
    {
        if (hasTriggered || objectToMove == null)
        {
            return;
        }

        hasTriggered = true;

        if (moveSound != null && AudioManager.instance != null)
        {
            AudioManager.instance.sfxSource.PlayOneShot(moveSound);
        }

        StartCoroutine(MoveInStages());
    }

    private IEnumerator MoveInStages()
    {
        foreach (Vector3 stepOffset in moveSteps)
        {
            float elapsedTime = 0f;
            Vector3 startingPos = objectToMove.transform.position;
            Vector3 targetPos = startingPos + stepOffset;

            while (elapsedTime < durationPerStep)
            {
                objectToMove.transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / durationPerStep);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            objectToMove.transform.position = targetPos;

            if (delayBetweenSteps > 0)
            {
                yield return new WaitForSeconds(delayBetweenSteps);
            }
        }

        if (nextMoverToTrigger != null)
        {
            nextMoverToTrigger.StartMoving();
        }
    }
}