using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PecaQuebraCabeca : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Transform posicaoCorreta;
    public float distanciaEncaixe = 0.5f;

    private Vector3 posicaoInicial;
    private Vector3 offset;
    private bool encaixada = false;
    private Camera cam;

    public bool Encaixada => encaixada;

    void Start()
    {
        posicaoInicial = transform.position;
        cam = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (encaixada) return;

        Vector3 mousePos = eventData.position;
        mousePos.z = cam.WorldToScreenPoint(transform.position).z;
        offset = transform.position - cam.ScreenToWorldPoint(mousePos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (encaixada) return;

        Vector3 mousePos = eventData.position;
        mousePos.z = cam.WorldToScreenPoint(transform.position).z;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        transform.position = worldPos + offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (encaixada) return;

        if (Vector3.Distance(transform.position, posicaoCorreta.position) < distanciaEncaixe)
        {
            transform.position = posicaoCorreta.position;
            encaixada = true;
            PuzzleQuebraCabeca.Instance.VerificarTodasPecas();
        }
        else
        {
            transform.position = posicaoInicial;
        }
    }
}
