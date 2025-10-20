using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColetaItem : MonoBehaviour
{
    public enum TipoItem
    {
        PauBrasil,
    }

    public TipoItem tipoItem;
    public int valorItem = 1;

    private SpriteRenderer spriteRenderer;

    public Sprite spritePadrao;
    public Sprite spriteDestacado;

    [Header("Referência do Jogador")]
    public Animator jogadorAnimator; // referência pro Animator do jogador
    private Transform jogadorTransform; // para saber a posição e flipar
    private bool olhandoDireita = true; // controle de direção atual

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && spritePadrao != null)
        {
            spriteRenderer.sprite = spritePadrao;
        }

        // 🔎 Encontra o jogador automaticamente
        if (jogadorAnimator == null)
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
        }
    }

    public void DestacaItem()
    {
        if (spriteRenderer != null && spriteDestacado != null)
        {
            spriteRenderer.sprite = spriteDestacado;
        }
        else
        {
            Debug.LogWarning("Não foi possível trocar o sprite. Verifique se o SpriteRenderer e o spriteDestacado estão atribuídos.");
        }
    }

    public void ItemPadrao()
    {
        if (spriteRenderer != null && spritePadrao != null)
        {
            spriteRenderer.sprite = spritePadrao;
        }
        else
        {
            Debug.LogWarning("Não foi possível trocar o sprite. Verifique se o SpriteRenderer e o spritePadrao estão atribuídos.");
        }
    }

    public void Coleta()
    {
        if (GameManager.instancia.PossoColetarItem(tipoItem))
        {
            GameManager.instancia.ColetaItem(tipoItem, valorItem);

            // 🧭 Verifica a direção e vira o jogador antes da animação
            if (jogadorTransform != null)
                AjustarDirecaoDoJogador();

            // 🎬 Dispara a animação de coleta
            if (jogadorAnimator != null)
            {
                Debug.Log("🎬 Acionando trigger 'ColetarMadeira'");
                jogadorAnimator.ResetTrigger("idle");
                jogadorAnimator.SetTrigger("ColetarMadeira");
                StartCoroutine(VoltarIdle());
            }
            else
            {
                Debug.LogWarning("❌ JogadorAnimator está nulo!");
            }

            Destroy(gameObject);
        }
    }

    // ⏳ Retorna pro estado Idle depois de 0.8 segundos (tempo da animação)
    private IEnumerator VoltarIdle()
    {
        yield return new WaitForSeconds(0.8f);
        if (jogadorAnimator != null)
        {
            jogadorAnimator.ResetTrigger("ColetarMadeira");
            jogadorAnimator.SetTrigger("idle");
        }
    }

    // === FUNÇÃO DE FLIP DO JOGADOR ===
    private void AjustarDirecaoDoJogador()
    {
        if (jogadorTransform == null) return;

        // Vira o jogador para o item antes de animar
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

