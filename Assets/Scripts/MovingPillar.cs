using UnityEngine;
using System.Collections;

public class MovingPillar : MonoBehaviour
{
    [Tooltip("The pillar or platform that will move.")]
    [SerializeField] private GameObject pillarToMove;

    [Tooltip("How far and in what direction the pillar will move. (0, -3, 0) means 3 units down.")]
    [SerializeField] private Vector3 moveOffset = new Vector3(0, -3f, 0);

    [Tooltip("How long the movement takes (in seconds).")]
    [SerializeField] private float moveDuration = 1.0f;

    [Tooltip("The sound to play when the pillar moves.")]
    [SerializeField] private AudioClip moveSound;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            if (moveSound != null && AudioManager.instance != null)
            {
                AudioManager.instance.sfxSource.PlayOneShot(moveSound);
            }

            // --- Start the Coroutine ---
            StartCoroutine(MovePillar());
        }
    }

    private IEnumerator MovePillar()
    {
        float elapsedTime = 0f;
        Vector3 startingPos = pillarToMove.transform.position;
        Vector3 targetPos = startingPos + moveOffset;

        while (elapsedTime < moveDuration)
        {
            pillarToMove.transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / moveDuration);
            
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        pillarToMove.transform.position = targetPos;
    }
}