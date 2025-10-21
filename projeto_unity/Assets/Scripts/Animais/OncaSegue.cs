using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OncaSegue : MonoBehaviour
{
    public float distanciaMaxima = 5f;
    public float velocidade = 2f;
    public float danoPlayer = 25f;

    private bool seguir = false;
    public GameObject playerObj;
    private Transform playerTransform;
    private PlayerVida playerVida;

    void Start()
    {
        playerTransform = playerObj.transform;
        playerVida = playerObj.GetComponent<PlayerVida>();
        IniciarSeguir();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= distanciaMaxima)
        {
            float step = velocidade * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);

            if (Vector3.Distance(transform.position, playerTransform.position) < 0.001f)
            {
                playerVida.ReduzirVida(danoPlayer);
            }
        }
    }

    public void IniciarSeguir() => seguir = true;

    public void PararSeguir()
    {
        seguir = false;
    }
}

