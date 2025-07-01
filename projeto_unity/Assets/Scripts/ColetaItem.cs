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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && spritePadrao != null)
        {
            spriteRenderer.sprite = spritePadrao;
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

    // Método para trocar de volta para o sprite padrão
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
            Destroy(gameObject);
        }
    }
}

