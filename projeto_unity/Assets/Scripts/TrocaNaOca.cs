using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaNaOca : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bau"))
        {
            Debug.Log("Colidiu com objeto da tag 'MinhaTag'");
        }
    }
}
