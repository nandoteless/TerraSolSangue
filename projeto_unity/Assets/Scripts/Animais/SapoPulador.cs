using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity; 

public class SapoPulador : MonoBehaviour
{
    public Vector2[] posicoesDePulo; 
    private int indiceAtual = 0;

    [EventRef]
    public string eventoSomPulo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checa se quem colidiu foi o player
        if (collision.gameObject.CompareTag("Player"))
        {
            PularParaProximaPosicao();
        }
    }

    void PularParaProximaPosicao()
    {
        if (posicoesDePulo.Length == 0) return;

        // Define a nova posição
        transform.position = posicoesDePulo[indiceAtual];

        // Toca o som de pulo do sapo
        if (!string.IsNullOrEmpty(eventoSomPulo))
        {
            RuntimeManager.PlayOneShot(eventoSomPulo, transform.position);
        }

        // Avança para a próxima posição (loop infinito)
        indiceAtual = (indiceAtual + 1) % posicoesDePulo.Length;
    }
}
