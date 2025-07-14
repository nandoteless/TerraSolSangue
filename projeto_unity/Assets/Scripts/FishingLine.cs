using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
  public GameObject anzol;  // Arraste o objeto "Anzol" aqui no Unity Inspector
    private LineRenderer lineRenderer;

    void Start()
    {
        // Esconde apenas a seta do cursor, mas mantém o controle do mouse
        Cursor.visible = false;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Linha com dois pontos
        lineRenderer.SetPosition(0, transform.position); // Base da vara

        // Definindo a cor da linha para preta
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;

        // Caso queira mudar a largura da linha
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    void Update()
    {
        // A posição do mouse segue a movimentação dele
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // Garantindo que a coordenada Z fique em 0, para não distorcer a linha

        // Movendo o anzol
        anzol.transform.position = mousePosition;
        lineRenderer.SetPosition(1, mousePosition);
    }

    // Quando a aplicação for fechada, mostrar novamente o cursor
    private void OnApplicationQuit()
    {
        Cursor.visible = true;
    }
}