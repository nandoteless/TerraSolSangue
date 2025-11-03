using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Texto")]
    public float delay = 0.02f;
    [TextArea] public string fullText;
    private string currentText = "";
    public TextMeshProUGUI textComponent;

    [Header("Som de Diálogo")]
    public AudioSource audioSource;
    public AudioClip blipSound;
    [Range(0.8f, 1.2f)] public float pitchVariation = 0.1f;
    public float volume = 0.6f;

    void Start()
    {
        if (textComponent == null)
        {
            Debug.LogError("⚠️ Text Component não atribuído!");
            return;
        }

        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        if (string.IsNullOrEmpty(fullText))
        {
            Debug.LogWarning("⚠️ Campo Full Text está vazio!");
            yield break;
        }

        textComponent.text = ""; // garante que começa vazio

        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textComponent.text = currentText;

            // Som por letra
            if (blipSound != null && audioSource != null)
            {
                char c = fullText[i];
                if (char.IsLetter(c))
                {
                    audioSource.pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation);
                    audioSource.PlayOneShot(blipSound, volume);
                }
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
