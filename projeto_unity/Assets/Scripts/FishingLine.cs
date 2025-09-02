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
    private Vector3 touchPosition;

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
        // --- Captura toque na tela (Mobile) ---
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            touchPosition = Camera.main.ScreenToWorldPoint(touchPos);
            touchPosition.z = 0f;

                // move o anzol até o ponto tocado
                anzol.transform.position = Vector3.MoveTowards(
                anzol.transform.position,
                touchPosition,
                speed * Time.deltaTime
            );

            lineRenderer.SetPosition(1, anzol.transform.position);
        }

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

        // Botão de pescar (tecla E no PC ou toque na tela)
        if (Keyboard.current.eKey.wasPressedThisFrame ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame))
        {
            OnPress();
        }
    }

    void OnPress()
    {
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Peixe"))
        {
            Destroy(hit.collider.gameObject);
            peixesPescados++;
            Debug.Log("Peixes pescados: " + peixesPescados);
        }
    }
}
