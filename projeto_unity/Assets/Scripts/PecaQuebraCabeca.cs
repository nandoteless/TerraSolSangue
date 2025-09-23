using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class PecaQuebraCabeca : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 posicaoInicial;
    private Vector3 offset;

    public Transform posicaoCorreta; // onde a peça deve ficar

    private bool encaixada = false;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (encaixada) return;
        offset = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        offset.z = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (encaixada) return;
        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position) + offset;
        pos.z = 0;
        transform.position = pos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (encaixada) return;

        // Verifica se está perto da posição correta
        if (Vector3.Distance(transform.position, posicaoCorreta.position) < 0.5f)
        {
            transform.position = posicaoCorreta.position;
            encaixada = true;
            Debug.Log("Peça encaixada!");
            
            PuzzleQuebraCabeca.Instance.VerificarTodasPecas();
        }
        else
        {
            // Volta para posição inicial se não encaixou
            transform.position = posicaoInicial;
        }
    }
}
