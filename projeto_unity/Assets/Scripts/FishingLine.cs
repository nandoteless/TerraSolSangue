using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Novo Input System

public class FishingLine : MonoBehaviour
{
    public GameObject anzol;  
    private LineRenderer lineRenderer;
    public float speed = 5f;
    private int peixesPescados = 0;
    public int totalPeixesParaFase2 = 3;
    private Vector3 targetPosition; // posição alvo (mouse ou toque)

    void Start()
    {
        Cursor.visible = true; // mostra o cursor no PC

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        targetPosition = anzol.transform.position;
    }

    void Update()
    {
        // --- Movimento com mouse (PC) ---
        if (Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
            targetPosition.z = 0f;
        }

        // --- Movimento com toque (Mobile) ---
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            targetPosition = Camera.main.ScreenToWorldPoint(touchPos);
            targetPosition.z = 0f;
        }

        // Move o anzol até a posição alvo
        anzol.transform.position = Vector3.MoveTowards(
            anzol.transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // Atualiza a linha
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, anzol.transform.position);

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

        // --- PESCAR (clique do mouse ou toque na tela) ---
        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame))
        {
            OnPress();
        }
    }

    void OnPress()
    {
        RaycastHit2D hit = Physics2D.Raycast(anzol.transform.position, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Peixe"))
        {
            Destroy(hit.collider.gameObject);
            peixesPescados++;
            Debug.Log("Peixes pescados: " + peixesPescados);
        }
    }
}
