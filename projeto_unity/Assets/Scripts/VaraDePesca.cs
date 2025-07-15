using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VaraDePesca : MonoBehaviour
{
    [SerializeField] private string nomeDaCena = "PescaCena";  // Altere para o nome da sua cena
    private bool cenaLiberada = false;

    private void Start()
    {
        // Desativa a vara no início, se os objetivos ainda não foram concluídos
        if (!GameManager.instancia.ObjetivosConcluidos())
        {
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            cenaLiberada = true;
        }
    }

    private void Update()
    {
        // Ativa a vara se os objetivos forem concluídos enquanto o jogo roda
        if (!cenaLiberada && GameManager.instancia.ObjetivosConcluidos())
        {
            cenaLiberada = true;
            GetComponent<Collider2D>().enabled = true;
            Debug.Log("Vara desbloqueada! Pode passar para a próxima cena.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (cenaLiberada && collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}