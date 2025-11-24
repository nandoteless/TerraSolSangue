using UnityEngine;

public class DanoNoBartolomeu : MonoBehaviour
{
    public float dano = 15f;
    public float intervaloDano = 1f;

    private float tempoUltimoDano = 0f;
    private BartolomeuVida vidaAtual;

    private bool estaColidindo = false; // se o player está encostando

    void Start()
    {
        vidaAtual = GetComponent<BartolomeuVida>();
        if (vidaAtual == null)
        {
            Debug.LogError("BartolomeuVida não encontrado!");
        }
    }

    public void OnAttack()
    {
        if (!estaColidindo) return; // só dá dano se estiver colidindo
        
        if (Time.time - tempoUltimoDano >= intervaloDano)
        {
            tempoUltimoDano = Time.time;

            if (vidaAtual != null)
            {
                vidaAtual.ReduzirVida(dano);
                Debug.Log("Bartolomeu levou dano: " + dano);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            estaColidindo = true;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            estaColidindo = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            estaColidindo = false;
    }
}
