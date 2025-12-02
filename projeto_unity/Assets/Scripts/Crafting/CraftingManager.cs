using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;
    public Image customCursor;
    public Slot[] craftingSlot;
    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public Slot resultSlot;
    public int totalItens;
    private int currentItensCrafted;

    void Start() 
    {
        currentItensCrafted = 0;
    }
    void Update() 
    {
        if (Input.GetMouseButtonUp(0))
        {
            // verifica se estamos segurando um item
            if (currentItem != null)
            {
                // aqui soltamos o item
                customCursor.gameObject.SetActive(false);
                // aqui vamos encontrar o slot mais próximo para incluir o item pela distância
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;
                foreach (Slot slot in craftingSlot)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);
                    if (dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }
                // agora vamos adicionar o elemento ao slot, alterando a imagem do filho e ativando ele 
                nearestSlot.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                nearestSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;
                // agora vamos preencher a lista de itens, que será utilizada para fazer o crafting
                itemList[nearestSlot.index] = currentItem;
                // aqui limpamos a variável que contém o item que estamos segurando
                currentItem = null;
                // chamamos a função para verificar a receita
                CheckForCreatedRecipes();
            }
        }
    }

    void CheckForCreatedRecipes()
    {
        // inicializamos as variáveis
         resultSlot.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        resultSlot.item = null;
        string currentRecipeString = "";

        // aqui vamos preenchendo de acordo com os itens da lista
        foreach(Item item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;
            }
            else // caso não tenha item na posição da lista preenchemos com null
            {
                currentRecipeString += "null";
            }
        }

        // agora vamos verificar se a string da receita montada é igual a alguma das strings cadastradas
        for (int i=0; i<recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                resultSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
                // ao acertar incrementa total de acertos
                currentItensCrafted++;
                // verifica se atingiu a quantidade total de itens
                if (currentItensCrafted >= totalItens)
                {
                    // verificar se o que fazer
                    Debug.Log("******************** Conseguiu armas - ver o que fazer ********************");
                }
                
            }
        }
    }

    public void OnClickSlot(Slot slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }

    public void OnMouseDownItem(Item item)
    {
        if (currentItem  == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }
}
