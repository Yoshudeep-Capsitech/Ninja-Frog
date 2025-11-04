using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [Tooltip("Time after falling before it starts to fade.")]
    public float fadeDelay = 0.5f;

    [Tooltip("How long it takes to fade out completely.")]
    public float fadeDuration = 1.0f;

    [Tooltip("How fast the platform falls. 1 = default gravity, 4 = 4x gravity.")]
    public float fallGravityScale = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void StartFall()
    {
        AudioManager.instance.PlayPlatformCrumbleSound();
        
        rb.gravityScale = fallGravityScale;
        rb.bodyType = RigidbodyType2D.Dynamic;
        //so the player's GroundCheck doesn't detect it as "Ground" while it's falling.
        gameObject.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeDelay);

        float timer = 0f;
        Color originalColor = sr.color; 
        while (timer < fadeDuration)
        {
            float progress = timer / fadeDuration;
            float newAlpha = Mathf.Lerp(1f, 0f, progress);
            
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            timer += Time.deltaTime;
            yield return null; 
        }

        Destroy(gameObject);
    }
}