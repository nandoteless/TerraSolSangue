using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OncaSegue : MonoBehaviour
{
   public float distanciaMaxima = 5f;
    public float velocidade = 2f;
    public float danoPlayer = 25f;

    private bool seguir = true;

    private Transform playerTransform;
    private PlayerVida playerVida;

    void Start()
    {
        // Acha automaticamente o player
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
    }

    void Update()
    {
        if (!seguir || playerTransform == null) 
            return;

        float distancia = Vector3.Distance(transform.position, playerTransform.position);

        if (distancia <= distanciaMaxima)
        {
            float step = velocidade * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);

            if (distancia < 0.5f)
            {
                // Dano ao player se encostar
                if (playerVida != null)
                    playerVida.ReduzirVida(danoPlayer);
            }
        }
    }

    public void PararSeguir()
    {
        seguir = false;
    }
}

