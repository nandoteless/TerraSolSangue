using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColetaItem : MonoBehaviour
{
    public enum TipoItem
    {
        PauBrasil,
        Cipo,     // ✅ Novo item
        Pedra     // ✅ Novo item
    }

    public TipoItem tipoItem;
    public int valorItem = 1;

    private SpriteRenderer spriteRenderer;

    public Sprite spritePadrao;
    public Sprite spriteDestacado;

    [Header("Referência do Jogador")]
    public Animator jogadorAnimator = null;
    private Transform jogadorTransform;
    private bool olhandoDireita = true;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && spritePadrao != null)
        {
            spriteRenderer.sprite = spritePadrao;
        }

        // Removido por DMVS porque essa animação só existe se for coletar Pau Brasil
        // Nesse caso ela deve ser preenchida via Inspector
        // 🔎 Localiza automaticamente o jogador
        /*if (jogadorAnimator == null)
        {
            GameObject jogador = GameObject.FindGameObjectWithTag("Player");
            if (jogador != null)
            {
                jogadorAnimator = jogador.GetComponent<Animator>();
                jogadorTransform = jogador.transform;
            }
        }

        if (jogadorAnimator == null)
        {
            Debug.LogError("⚠️ Nenhum Animator do jogador encontrado! Verifique a tag 'Player' e o componente Animator.");
        }*/
    }

    public void DestacaItem()
    {
        if (spriteRenderer != null && spriteDestacado != null)
            spriteRenderer.sprite = spriteDestacado;
        else
            Debug.LogWarning("Não foi possível trocar o sprite para destacado.");
    }

    public void ItemPadrao()
    {
        if (spriteRenderer != null && spritePadrao != null)
            spriteRenderer.sprite = spritePadrao;
        else
            Debug.LogWarning("Não foi possível trocar o sprite para padrão.");
    }

    public void Coleta()
    {
        if (GameManager.instancia.PossoColetarItem(tipoItem))
        {
            GameManager.instancia.ColetaItem(tipoItem, valorItem);

            // 🧭 Vira o jogador para o item antes da animação
            if (jogadorTransform != null)
                AjustarDirecaoDoJogador();

            // 🎬 Dispara a animação de coleta caso haja animação para o item
            if (jogadorAnimator != null)
            {
                Debug.Log("🎬 Acionando trigger 'ColetarMadeira'");
                // jogadorAnimator.ResetTrigger("idle");
                jogadorAnimator.SetTrigger("ColetarMadeira");
                // StartCoroutine(VoltarIdle());
            }
            // Comentado por DMVS porque tem itens que não possuem animação
            // else
            // {
            //     Debug.LogWarning("❌ JogadorAnimator está nulo!");
            // }

            Destroy(gameObject);
        }
    }

    // private IEnumerator VoltarIdle()
    // {
    //     yield return new WaitForSeconds(0.8f);

    //     if (jogadorAnimator != null)
    //     {
    //         jogadorAnimator.ResetTrigger("ColetarMadeira");
    //         jogadorAnimator.SetTrigger("idle");
    //     }
    // }

    private void AjustarDirecaoDoJogador()
    {
        if (jogadorTransform == null) return;

        bool itemADireita = transform.position.x > jogadorTransform.position.x;

        if (itemADireita && !olhandoDireita)
            VirarJogador(true);
        else if (!itemADireita && olhandoDireita)
            VirarJogador(false);
    }

    private void VirarJogador(bool olharDireita)
    {
        olhandoDireita = olharDireita;

        Vector3 escala = jogadorTransform.localScale;
        escala.x = Mathf.Abs(escala.x) * (olharDireita ? 1 : -1);
        jogadorTransform.localScale = escala;
    }
}
