using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{
    [SerializeField] private string nomeCena = "NomeDaCena"; // defina no Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // só troca se for o jogador
        {
            SceneManager.LoadScene(nomeCena);
        }
    }
}
