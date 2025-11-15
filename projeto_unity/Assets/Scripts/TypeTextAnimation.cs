using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class TypeTextAnimation : MonoBehaviour
{
    public Action TypeFinished;

    [Header("Typing Settings")]
    public float typeDelay = 0.05f;
    public TextMeshProUGUI textObject;
    public string fullText;

    [Header("Sound")]
    public AudioSource audioSource;   
    public AudioClip typeSound;       

    private Coroutine coroutine;

    public void StartTyping()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textObject.text = fullText;
        textObject.maxVisibleCharacters = 0;

        for (int i = 0; i <= textObject.text.Length; i++)
        {
            textObject.maxVisibleCharacters = i;

            // 🔊 Tocar som a cada letra
            if (typeSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typeSound);
            }

            yield return new WaitForSeconds(typeDelay);
        }

        TypeFinished?.Invoke();
    }

    public void Skip()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        textObject.maxVisibleCharacters = textObject.text.Length;
    }
}
