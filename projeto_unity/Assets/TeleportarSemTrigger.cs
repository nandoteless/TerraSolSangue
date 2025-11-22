using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportarSemTrigger : MonoBehaviour
{
    public Transform destino; // arraste o transform de destino no Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.position = destino.position;
        }
    }
}
