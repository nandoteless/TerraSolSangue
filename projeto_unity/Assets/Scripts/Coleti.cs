using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleti : MonoBehaviour

{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrou no trigger com: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Coletado e destruído!");
            Destroy(gameObject);
        }
    }
}




