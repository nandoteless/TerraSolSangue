using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Novo Input System

public class FishingLine : MonoBehaviour
{
    public GameObject anzol;  
    private LineRenderer lineRenderer;

    private int peixesPescados = 0;
    public int totalPeixesParaFase2 = 3;

    private Vector3 mousePosition; // Agora é global para ser usada em qualquer função

    void Start()
    {
        Cursor.visible = false;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    void Update()
    {
        // Pega posição do mouse pelo novo Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        mousePosition.z = 0f;

        anzol.transform.position = mousePosition;
        lineRenderer.SetPosition(1, mousePosition);

        // Troca de fase
        if (peixesPescados >= totalPeixesParaFase2)
        {
            MusicaController musica = FindObjectOfType<MusicaController>();
            if (musica != null)
            {
                musica.PararMusica();
            }
            SceneManager.LoadScene("Fase2");
        }

        // Botão de pescar
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            OnPress();
        }
    }

    void OnPress()
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Peixe"))
        {
            Destroy(hit.collider.gameObject);
            peixesPescados++;
            Debug.Log("Peixes pescados: " + peixesPescados);
        }
    }
}
