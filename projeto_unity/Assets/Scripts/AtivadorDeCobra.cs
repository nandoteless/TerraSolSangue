using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorDeCobra : MonoBehaviour
{
    public GameObject cobra; // Referência ao GameObject da cobra

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cobra.GetComponent<CobraSeguidora>().IniciarSeguir();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cobra.GetComponent<CobraSeguidora>().PararSeguir();
        }
    }
}