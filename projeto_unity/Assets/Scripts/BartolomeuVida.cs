using UnityEngine;
using UnityEngine.UI;

public class BartolomeuVida : MonoBehaviour
{
    [Header("UI")]
    public Slider barraVida; // Arraste o Slider (World Space) aqui

    [Header("Atributos")]
    public float vidaMaxima = 150f;
    public float vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;

        // Configuração inicial da Barra de Vida
        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vidaAtual;
        }
    }

    public void ReduzirVida(float dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        // Atualiza a barra visualmente
        if (barraVida != null)
        {
            barraVida.value = vidaAtual;
        }

        if (vidaAtual <= 0)
            Morrer();
    }

    void Morrer()
    {
        // Desativa a barra de vida para ela sumir junto com a "morte" lógica
        if (barraVida != null) barraVida.gameObject.SetActive(false);

        // Desativa a IA
        if (GetComponent<BartolomeuIA>() != null)
            GetComponent<BartolomeuIA>().enabled = false;
            
        // Destrói o objeto após 2 segundos
        Destroy(gameObject, 2f);
    }
}