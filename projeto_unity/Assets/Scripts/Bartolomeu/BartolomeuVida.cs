using UnityEngine;
using UnityEngine.UI;

public class BartolomeuVida : MonoBehaviour
{
    [Header("UI")]
    public Slider barraVida;
    [Header("Atributos")]
    public float vidaMaxima = 150f;
    public float vidaAtual;

    // Variáveis internas
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();

        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vidaAtual;
        }
    }

    public void ReduzirVida(float dano)
    {
        if (vidaAtual <= 0) return;

        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        if (barraVida != null)
            barraVida.value = vidaAtual;

        if (vidaAtual <= 0)
            Morrer();
    }

    void Morrer()
    {
        if (barraVida != null) barraVida.gameObject.SetActive(false);
        if (GetComponent<BartolomeuIA>() != null) GetComponent<BartolomeuIA>().enabled = false;
    }
}