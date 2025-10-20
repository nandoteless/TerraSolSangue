using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para trocar de cena

public class PuzzleQuebraCabeca : MonoBehaviour
{
    public static PuzzleQuebraCabeca Instance;

    [Header("Peças do Quebra-Cabeça")]
    public PecaQuebraCabeca[] pecas;

    [Header("Baú e Cena")]
    public GameObject bau; // Baú que será desbloqueado
    public string nomeCenaParaCarregar; // Nome da cena que será carregada

    void Awake()
    {
        // Configura o singleton (garante apenas uma instância)
        if (Instance == null)
        {
            Instance = this;
            if (bau != null)
                bau.SetActive(false);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Verifica se todas as peças estão na posição correta
    public void VerificarTodasPecas()
    {
        foreach (var peca in pecas)
        {
            // Usa Distance para tolerar pequenas diferenças de posição
            if (Vector3.Distance(peca.transform.position, peca.posicaoCorreta.position) > 0.05f)
                return; // ainda há peças fora do lugar
        }

        Debug.Log("✅ Puzzle completo! Baú desbloqueado!");

        if (bau != null)
            bau.SetActive(true);

        // Troca de cena após um pequeno atraso
        StartCoroutine(TrocarCenaComDelay(2f));
    }

    // Aguarda alguns segundos antes de carregar a próxima cena
    private IEnumerator TrocarCenaComDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!string.IsNullOrEmpty(nomeCenaParaCarregar))
        {
            SceneManager.LoadScene(nomeCenaParaCarregar);
        }
        else
        {
            Debug.LogWarning("⚠️ Nome da cena para carregar não foi definido.");
        }
    }
}
