using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingLine : MonoBehaviour
{
   public GameObject anzol;  // Arraste o objeto "Anzol" aqui no Unity Inspector
    private LineRenderer lineRenderer;

      // Contador de peixes pescados
    private int peixesPescados = 0;
    public int totalPeixesParaFase2 = 3;  // Quantos peixes são necessários para passar para a fase 2

    void Start()
    {
        // Esconde apenas a seta do cursor, mas mantém o controle do mouse
        Cursor.visible = false;

        lineRenderer = GetComponent<LineRenderer>();
     lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;  // Linha com dois pontos

       

        // Caso queira mudar a largura da linha
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

  void Update()
    {
        // Atualiza a posição do anzol
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        anzol.transform.position = mousePosition;
        lineRenderer.SetPosition(1, mousePosition);

        // Verifica se o botão 'E' foi pressionado e o cursor está sobre o peixe
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Se o Raycast atingir um objeto com a tag "Peixe"
            if (hit.collider != null && hit.collider.CompareTag("Peixe"))
            {
                // Destrói o peixe
                Destroy(hit.collider.gameObject);

                // Atualiza a contagem de peixes pescados
                peixesPescados++;
                Debug.Log("Peixes pescados: " + peixesPescados);

                // Se o jogador pescar 3 peixes, troca para a fase 2
                if (peixesPescados >= totalPeixesParaFase2)
                {
                    SceneManager.LoadScene("Fase2");  // Troca para a cena da fase 2
                }
            }
        }
    }
}