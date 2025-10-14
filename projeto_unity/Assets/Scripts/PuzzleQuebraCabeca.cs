using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Adicionado para troca de cena

public class PuzzleQuebraCabeca : MonoBehaviour
{
    public static PuzzleQuebraCabeca Instance;
    public PecaQuebraCabeca[] pecas;
    public GameObject bau; // baú que será desbloqueado
    public string nomeCenaParaCarregar; // Nome da cena que será carregada

    void Awake()
    {
        Instance = this;
        if (bau != null) bau.SetActive(false);
    }

    public void VerificarTodasPecas()
    {
        foreach (var peca in pecas)
        {
            if (!peca.transform.position.Equals(peca.posicaoCorreta.position))
                return; // ainda faltam peças
        }

        Debug.Log("Puzzle completo! Baú desbloqueado!");
        if (bau != null) bau.SetActive(true);

        // Opcional: esperar alguns segundos antes de trocar de cena
        StartCoroutine(TrocarCenaComDelay(2f));
    }

    private IEnumerator TrocarCenaComDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!string.IsNullOrEmpty(nomeCenaParaCarregar))
        {
            SceneManager.LoadScene(nomeCenaParaCarregar);
        }
        else
        {
            Debug.LogWarning("Nome da cena para carregar não foi definido.");
        }
    }
}
