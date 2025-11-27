using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MatarOncaPorTecla : MonoBehaviour
{
   [Header("Configuração")]
    public float distanciaMaxima = 5f;
    public GameObject objetoSubstituto;  // O que aparece depois que a onça morre

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
    }

    // Chamado pelo Input System (ataque)
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AtacarOnca();
    }

    void AtacarOnca()
    {
        GameObject oncaMaisProxima = null;
        float menorDistancia = Mathf.Infinity;

        GameObject[] oncas = GameObject.FindGameObjectsWithTag("onca");

        foreach (GameObject o in oncas)
        {
            float distancia = Vector2.Distance(player.position, o.transform.position);

            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                oncaMaisProxima = o;
            }
        }

        if (oncaMaisProxima != null && menorDistancia <= distanciaMaxima)
        {
            SubstituirOnca(oncaMaisProxima);
        }
        else
        {
            Debug.Log("Nenhuma onça está perto o suficiente para atacar.");
        }
    }

    void SubstituirOnca(GameObject oncaAlvo)
    {
        OncaSegue script = oncaAlvo.GetComponent<OncaSegue>();
        if (script != null)
            script.PararSeguir();

        Vector3 posicao = oncaAlvo.transform.position;
        Quaternion rotacao = oncaAlvo.transform.rotation;

        Destroy(oncaAlvo);

        if (objetoSubstituto != null)
            Instantiate(objetoSubstituto, posicao, rotacao);

        Debug.Log("Onça eliminada!");
    }
}
