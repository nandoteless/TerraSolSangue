using UnityEngine;
using UnityEngine.UI;

public class BartolomeuVida : MonoBehaviour
{
    public float vidaMaxima = 150f;
    public float vidaAtual;
    public Slider barraDeVida;

    void Start()
    {
        vidaAtual = vidaMaxima;

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaMaxima;
        }
    }

    public void ReduzirVida(float dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        if (barraDeVida != null)
            barraDeVida.value = vidaAtual;

        if (vidaAtual <= 0)
            Morrer();
    }

    void Morrer()
    {
        GetComponent<BartolomeuIA>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 2f);
    }
}
