using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ColetaComida : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // faz a comida sumir
        }
    }
}


