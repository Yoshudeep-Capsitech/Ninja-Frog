using UnityEngine;
using System.Collections; 

public class SwooshBlock : MonoBehaviour
{
    [Tooltip("The platform block that will move out of the way.")]
    [SerializeField] private GameObject blockToMove;

    [Tooltip("How far and in what direction the block will move. (X: -5) means 5 units left.")]
    [SerializeField] private Vector3 moveOffset = new Vector3(-5f, 0, 0);

    [Tooltip("How fast the block moves (in seconds). Smaller is faster! 0.3 is a good 'swoosh'.")]
    [SerializeField] private float swooshDuration = 0.3f;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            AudioManager.instance.PlaySwooshSound();

            StartCoroutine(MoveBlock());
        }
    }

    private IEnumerator MoveBlock()
    {
        float elapsedTime = 0f;
        Vector3 startingPos = blockToMove.transform.position;
        Vector3 targetPos = startingPos + moveOffset;

        while (elapsedTime < swooshDuration)
        {
            blockToMove.transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / swooshDuration);
            
            elapsedTime += Time.deltaTime;

            yield return null; 
        }


        blockToMove.transform.position = targetPos;

        if (blockToMove.TryGetComponent<Collider2D>(out var col))
        {
            col.enabled = false;
        }
    }
}