using UnityEngine;
using System.Collections;

public class ParentingPlatform : MonoBehaviour
{
    private Transform playerTransform = null;
    private Coroutine parentCoroutine = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            
            parentCoroutine = StartCoroutine(SetParentAfterFrame());
        }
    }

    private IEnumerator SetParentAfterFrame()
    {
        yield return new WaitForEndOfFrame();
        
        if (playerTransform != null && playerTransform.gameObject.activeInHierarchy)
        {
            playerTransform.SetParent(this.transform);
        }
        parentCoroutine = null;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (parentCoroutine != null)
            {
                StopCoroutine(parentCoroutine);
                parentCoroutine = null;
            }
            
            other.transform.SetParent(null);
            playerTransform = null;
        }
    }
}