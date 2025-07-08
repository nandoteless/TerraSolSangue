using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private ColetaItem nearestCollectible;

    void Update()
    {
        // Verifica se possui um coletável próximo e se pressionou a tecla para realizar a coleta
        if (nearestCollectible != null && InputManager.instancia.collect)
        {
            // Chama o método para coletar o item, script adicionado ao item coletável
            nearestCollectible.Coleta();

            nearestCollectible = null; // Limpa a referência após coletar
        }
    }

    // Método para detecção de proximidade
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o Collider que entra na área de proximidade é de um item coletável
        ColetaItem item = other.GetComponent<ColetaItem>();
        if (item != null)
        {
            if (GameManager.instancia.PossoColetarItem(item.tipoItem))
            {
                nearestCollectible = item; // Armazena a referência do item mais próximo
                nearestCollectible.DestacaItem(); // destaca o item    
            }
            
        }
    }
    
    // Método para detectar que saiu da proximidade de um item coletável
    void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o Collider que saiu da área de proximidade é de um item coletável
        ColetaItem item = other.GetComponent<ColetaItem>();
        if (item != null)
        {
            item.ItemPadrao(); // tira o destaque do item
            nearestCollectible = null; // Limpa a referência do item mais próximo
        }
    }
}