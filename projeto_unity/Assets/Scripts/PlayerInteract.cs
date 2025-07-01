using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private ColetaItem nearestCollectible;
    void Update()
    {
        // NOVO: Lógica de Interação (ex: "Pressione E para Coletar")
        if (nearestCollectible != null && InputManager.interact)
        {
            // Chama o método para coletar no item
            nearestCollectible.Coleta();

            nearestCollectible = null; // Limpa a referência após coletar

            // Você também pode querer desativar um ícone de interação aqui
            // UIManager.instance.ShowInteractionPrompt(false);
        }
    }

    // NOVO: Métodos para detecção de proximidade (Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Certifique-se de que o Collider que entra na área de proximidade é de um item coletável
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
    
    void OnTriggerExit2D(Collider2D other)
    {
        // Certifique-se de que o Collider que entra na área de proximidade é de um item coletável
        ColetaItem item = other.GetComponent<ColetaItem>();
        if (item != null)
        {
            item.ItemPadrao(); // tira o destaque do item
            nearestCollectible = null; // Armazena a referência do item mais próximo
        }
    }
}