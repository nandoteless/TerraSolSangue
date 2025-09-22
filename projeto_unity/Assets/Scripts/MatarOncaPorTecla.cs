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

    void Update()
    {
        if (Input.GetKeyDown(tecla))
        {
            Substituir();
        }
    }

    void Substituir()
    {
        if (onca != null)
        {
            // Guarda a posição e rotação originais
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
        else
        {
            Debug.LogWarning("Onça não atribuída no Inspector!");
        }
    }
}
