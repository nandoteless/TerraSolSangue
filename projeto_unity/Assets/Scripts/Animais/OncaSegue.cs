using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;

public class OncaSegue : MonoBehaviour
{
    public float distanciaMaxima = 5f;
    public float velocidade = 2f;
    public float danoPlayer = 25f;

    private bool seguir = true;
    private bool animacaoAtiva = false;

    private Transform playerTransform;
    private PlayerVida playerVida;
    private Animator anim;
    private SpriteRenderer sprite; // para flip

    void Start()
    {
        // Acha o Player automaticamente
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            playerVida = playerObj.GetComponent<PlayerVida>();
        }
        else
        {
            Debug.LogError("Nenhum objeto com tag 'Player' foi encontrado na cena.");
        }

        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!seguir || playerTransform == null)
            return;

        float distancia = Vector3.Distance(transform.position, playerTransform.position);

        // Ativa animação quando entra no raio
        if (distancia <= distanciaMaxima && !animacaoAtiva)
        {
            animacaoAtiva = true;
            if (anim != null)
                anim.SetBool("andando", true);
        }

        // Desativa animação quando sai do raio
        if (distancia > distanciaMaxima && animacaoAtiva)
        {
            animacaoAtiva = false;
            if (anim != null)
                anim.SetBool("andando", false);
        }

        // Se estiver na distância, segue o player
        if (distancia <= distanciaMaxima)
        {
            float step = velocidade * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);

            // FLIP automático (vira para o lado do player)
            if (sprite != null)
            {
                if (playerTransform.position.x > transform.position.x)
                    sprite.flipX = false; // olha para direita
                else
                    sprite.flipX = true; // olha para esquerda
            }

            // Dano ao encostar
            if (distancia < 0.5f)
            {
                if (playerVida != null)
                    playerVida.ReduzirVida(danoPlayer);
            }
        }
    }

    public void PararSeguir()
    {
        seguir = false;

        // Desliga animação
        if (anim != null)
            anim.SetBool("andando", false);
    }
}


