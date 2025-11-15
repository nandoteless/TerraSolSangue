using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeAndChangeScene : MonoBehaviour
{
    public string nextSceneName;       // Nome da pr�xima cena
    public float fadeDuration = 1f;    // Tempo do fade (segundos)

    private bool isFading = false;
    private SpriteRenderer playerRenderer;
    private float fadeTimer = 0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFading)
        {
            playerRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (playerRenderer != null)
            {
                isFading = true;
                fadeTimer = 0f;
            }
        }
    }

    void Update()
    {
        if (isFading && playerRenderer != null)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);

            Color newColor = playerRenderer.color;
            newColor.a = alpha;
            playerRenderer.color = newColor;

            if (fadeTimer >= fadeDuration)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}