using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatarOncaPorTecla : MonoBehaviour
{
    [Header("Referências")]
    public GameObject onca;              // Onça que será destruída
    public GameObject objetoSubstituto;  // O que aparece no lugar da onça

    [Header("Configuração")]
    public KeyCode tecla = KeyCode.F;    // Tecla para "matar" a onça
    public float distanciaMaxima = 3f;   // Distância máxima para acionar

    private Transform player;

    void Start()
    {
        // Acha o player pela tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(tecla) && player != null && onca != null)
        {
            float distancia = Vector2.Distance(player.position, onca.transform.position);

            if (distancia <= distanciaMaxima)
            {
                Substituir();
            }
            else
            {
                Debug.Log("Muito longe da onça para atacar!");
            }
        }
    }

    void Substituir()
    {
        if (onca != null)
        {
            // Para o script de seguir, se existir
            OncaSegue scriptSegue = onca.GetComponent<OncaSegue>();
            if (scriptSegue != null)
            {
                scriptSegue.PararSeguir();
            }

            // Guarda a posição e rotação
            Vector3 posicao = onca.transform.position;
            Quaternion rotacao = onca.transform.rotation;

            // Destroi a onça
            Destroy(onca);

            // Instancia o substituto
            if (objetoSubstituto != null)
            {
                Instantiate(objetoSubstituto, posicao, rotacao);
            }

            Debug.Log("Onça eliminada e substituída!");
        }
    }
}
