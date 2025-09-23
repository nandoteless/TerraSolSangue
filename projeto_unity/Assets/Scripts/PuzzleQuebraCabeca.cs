using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleQuebraCabeca : MonoBehaviour
{
    public static PuzzleQuebraCabeca Instance;
    public PecaQuebraCabeca[] pecas;
    public GameObject bau; // baú que será desbloqueado

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
    }
}
