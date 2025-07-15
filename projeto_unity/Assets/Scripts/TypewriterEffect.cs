using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.02f; // Tempo entre cada letra
    public string fullText; // O texto completo que será mostrado
    private string currentText = "";
    public TextMeshProUGUI textComponent;

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}