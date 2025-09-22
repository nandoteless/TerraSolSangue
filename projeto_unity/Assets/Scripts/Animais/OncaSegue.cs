using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OncaSegue : MonoBehaviour
{
    public float distanciaMaxima = 5f; 
    public float velocidade = 2f; 

    private bool seguir = false;
    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Nenhum objeto com a tag 'Player' foi encontrado!");
        }

        IniciarSeguir();
    }

    void FixedUpdate()
    {
        if (seguir && player != null)
        {
            float distancia = Vector2.Distance(transform.position, player.position);

            if (distancia <= distanciaMaxima)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                Vector2 newPosition = (Vector2)transform.position + direction * velocidade * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void IniciarSeguir() => seguir = true;

    public void PararSeguir()
    {
        seguir = false;
        rb.velocity = Vector2.zero;
    }
}

