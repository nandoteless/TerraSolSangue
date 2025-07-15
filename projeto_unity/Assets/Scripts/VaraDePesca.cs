using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Importante para usar TextMeshProUGUI


public class VaraDePesca : MonoBehaviour
{
 public GameObject painelAviso; // Painel com a mensagem de "complete os objetivos"
    public TextMeshProUGUI objetivo1Text; // TMP do primeiro objetivo
    public TextMeshProUGUI objetivo2Text; // TMP do segundo objetivo
    public string textoCompleto = "3/3"; // Texto alvo que indica que está completo

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifica se os dois TMPs estão com o texto "3/3"
            if (objetivo1Text.text == textoCompleto && objetivo2Text.text == textoCompleto)
            {
                SceneManager.LoadScene("Fase2"); // Troca para a próxima fase
            }
            else
            {
                // Mostra o painel de aviso
                painelAviso.SetActive(true);
                Invoke(nameof(EsconderPainel), 3f);
            }
        }
    }

    void EsconderPainel()
    {
        painelAviso.SetActive(false);
    }
}