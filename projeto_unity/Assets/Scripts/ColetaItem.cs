using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && spritePadrao != null)
        {
            spriteRenderer.sprite = spritePadrao;
        }

        // Tenta encontrar o jogador automaticamente pela tag
        if (jogadorAnimator == null)
        {
            GameObject jogador = GameObject.FindGameObjectWithTag("Player");
            if (jogador != null)
                jogadorAnimator = jogador.GetComponent<Animator>();
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
            Debug.LogWarning("Não foi possível trocar o sprite. Verifique se o SpriteRenderer e o alternateSprite estão atribuídos.");
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
            Debug.LogWarning("Não foi possível trocar o sprite. Verifique se o SpriteRenderer e o defaultSprite estão atribuídos.");
        }
    }

    public void Coleta()
    {
        if (GameManager.instancia.PossoColetarItem(tipoItem))
        {
            GameManager.instancia.ColetaItem(tipoItem, valorItem);

            // 🔥 Dispara a animação de coleta
            if (jogadorAnimator != null)
            {
                jogadorAnimator.ResetTrigger("Idle");
                jogadorAnimator.SetTrigger("ColetarMadeira");

                // 🔁 Força o retorno pro Idle depois da animação
                StartCoroutine(VoltarIdle());
            }

            Destroy(gameObject);
        }
    }

    // 🔄 Garante o retorno ao estado Idle depois de 0.8 segundos
    private IEnumerator VoltarIdle()
    {
        yield return new WaitForSeconds(0.8f); // duração da animação de coleta
        if (jogadorAnimator != null)
        {
            jogadorAnimator.ResetTrigger("ColetarMadeira");
            jogadorAnimator.SetTrigger("Idle");
        }
    }
}

