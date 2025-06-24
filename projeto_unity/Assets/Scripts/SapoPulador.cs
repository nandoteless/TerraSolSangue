using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapoPulador : MonoBehaviour
{
     public Vector2[] posicoesDePulo; // Adicione no Inspector as posições para onde o sapo pode ir
    private int indiceAtual = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checa se quem colidiu foi o player (certifique-se de colocar a Tag "Player" no seu jogador)
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

        // Avança para a próxima posição (loop infinito)
        indiceAtual = (indiceAtual + 1) % posicoesDePulo.Length;
    }
}