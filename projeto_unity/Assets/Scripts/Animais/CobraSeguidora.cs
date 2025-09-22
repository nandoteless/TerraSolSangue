using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraSeguidora : MonoBehaviour
{
      public Transform player; // Referência ao jogador
    public float distanciaMaxima = 5f; // Distância máxima para seguir
    public float velocidade = 2f; // Velocidade da cobra

    private bool seguir = false;

    void Update()
    {
        if (seguir)
        {
            float distancia = Vector2.Distance(transform.position, player.position);
            if (distancia <= distanciaMaxima)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * velocidade * Time.deltaTime);
            }
        }
    }

    public void IniciarSeguir()
    {
        seguir = true;
    }

    public void PararSeguir()
    {
        seguir = false;
    }
}