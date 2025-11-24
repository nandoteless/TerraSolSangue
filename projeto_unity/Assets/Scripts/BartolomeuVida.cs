using UnityEngine;
using UnityEngine.UI;

public class BartolomeuVida : MonoBehaviour
{
    public float vidaMaxima = 150f;
    public float vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void ReduzirVida(float dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);
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
