using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class AnimacaoDano : MonoBehaviour
{
    [Header("Objeto da animação de dano")]
    public GameObject efeitoDano; // arraste aqui o objeto com a animação (fica desativado na cena)

    public float duracao = 0.5f; // tempo que fica ativo

    public void AcionarAnimacao()
    {
        if (efeitoDano != null)
        {
            efeitoDano.SetActive(true);
            CancelInvoke(nameof(DesativarEfeito));
            Invoke(nameof(DesativarEfeito), duracao);
        }
        else
        {
            Debug.LogWarning("Efeito de dano não atribuído no Inspector!");
        }
    }

    void DesativarEfeito()
    {
        if (efeitoDano != null)
            efeitoDano.SetActive(false);
    }
}
