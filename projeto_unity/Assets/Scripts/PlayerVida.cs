using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerVida : MonoBehaviour
{
    public Slider vidaSlider;
    public float vidaMaxima = 100f;
    private float vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;

        if (vidaSlider != null)
        {
            vidaSlider.maxValue = vidaMaxima;
            vidaSlider.value = vidaAtual;
        }
        else
        {
            Debug.LogWarning("Slider de vida não atribuído!");
        }
    }

    public void ReduzirVida(float quantidade)
    {
        vidaAtual -= quantidade;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        if (vidaSlider != null)
        {
            vidaSlider.value = vidaAtual;
        }

        if (vidaAtual <= 0)
        {
            Debug.Log("GAME OVER");
            ReiniciarCena(); // Chama a função para reiniciar
        }
    }

    void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cobra"))
        {
            Debug.Log("Colidiu com a cobra (Trigger)!");
            ReduzirVida(25f); // Ajuste o valor conforme quiser
        }
    }

    // Se estiver usando colisão normal em vez de trigger:
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cobra"))
        {
            Debug.Log("Colidiu com a cobra (Collision)!");
            ReduzirVida(25f);
        }
    }
    */
}