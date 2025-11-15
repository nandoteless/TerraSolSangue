using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SapoPulador : MonoBehaviour
{
    public Vector2[] posicoesDePulo;
    private int indiceAtual = 0;

    public EventReference eventoSomPulo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PularParaProximaPosicao();
        }
    }

    void PularParaProximaPosicao()
    {
        if (posicoesDePulo.Length == 0) return;

        transform.position = posicoesDePulo[indiceAtual];

        // Toca o som usando EventReference
        if (eventoSomPulo.IsNull == false)
        {
            RuntimeManager.PlayOneShot(eventoSomPulo, transform.position);
        }

        indiceAtual = (indiceAtual + 1) % posicoesDePulo.Length;
    }
}
