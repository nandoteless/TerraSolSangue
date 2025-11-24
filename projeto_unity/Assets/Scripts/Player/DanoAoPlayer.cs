using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DanoAoPlayer : MonoBehaviour
{
    public float dano = 25f;          // quanto dano o player leva
    public float intervaloDano = 1f;  // tempo entre danos

    private float tempoUltimoDano = 0f;
    private PlayerVida playerVida;

    void Start()
    {
        playerVida = GetComponent<PlayerVida>();
        if (playerVida == null)
        {
            Debug.LogError("PlayerVida não encontrado no Player!");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Se encostar em inimigo (tag "Inimigo") e já passou o intervalo
        if (collision.gameObject.CompareTag("inimigo") && Time.time - tempoUltimoDano >= intervaloDano)
        {
            if (playerVida != null)
            {
                playerVida.ReduzirVida(dano);
            }
            tempoUltimoDano = Time.time;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Também funciona se inimigo usar trigger
        if (other.CompareTag("inimigo") && Time.time - tempoUltimoDano >= intervaloDano)
        {
            if (playerVida != null)
            {
                playerVida.ReduzirVida(dano);
            }
            tempoUltimoDano = Time.time;
        }
    }
}
