using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Necessário

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;
    public Image customCursor;
    public Slot[] craftingSlot;
    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public Slot resultSlot;

    // Variável para guardar a posição atual do toque ou do mouse
    private Vector2 currentInputPosition;

    void Update() 
    {
        // LÓGICA DE ARRASTAR VISUALMENTE
        // Se estamos segurando um item, o cursor deve seguir o dedo/mouse
        if (currentItem != null)
        {
            // Atualiza a posição da imagem do cursor para onde o dedo está
            customCursor.transform.position = currentInputPosition;
        }
    }

    // ---------------------------------------------------------
    // EVENTO 1: Ligue isso na Action de "Position" (Value - Vector2)
    // Isso atualiza a variável sempre que o dedo se move na tela
    // ---------------------------------------------------------
    public void OnPointerMove(InputAction.CallbackContext context)
    {
        // Lê a posição da tela (Vector2)
        currentInputPosition = context.ReadValue<Vector2>();
    }

    // ---------------------------------------------------------
    // EVENTO 2: Ligue isso na Action de "Press/Click" (Button)
    // Isso detecta quando você levanta o dedo
    // ---------------------------------------------------------
    public void OnPointerUp(InputAction.CallbackContext context)
    {
        // 'Canceled' significa que o botão foi solto ou o dedo saiu da tela
        if (context.canceled)
        {
            TryDropItem();
        }
    }

    private void TryDropItem()
    {
        if (currentItem != null)
        {
            // Desativa o cursor visual
            customCursor.gameObject.SetActive(false);

            Slot nearestSlot = null;
            float shortestDistance = float.MaxValue;

            // Usa a posição armazenada do toque (currentInputPosition)
            foreach (Slot slot in craftingSlot)
            {
                float dist = Vector2.Distance(currentInputPosition, slot.transform.position);
                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    nearestSlot = slot;
                }
            }

            // Se encontrou um slot e ele está próximo o suficiente (opcional: definir uma distância mínima)
            if (nearestSlot != null)
            {
                nearestSlot.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                nearestSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;
                
                itemList[nearestSlot.index] = currentItem;
                
                currentItem = null;
                CheckForCreatedRecipes();
            }
            else 
            {
                // Opcional: Se soltar no nada, reseta o currentItem para não travar
                currentItem = null;
            }
        }
    }

    // O resto do código permanece igual...
    void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        resultSlot.item = null;
        string currentRecipeString = "";

        foreach(Item item in itemList)
        {
            if (item != null) currentRecipeString += item.itemName;
            else currentRecipeString += "null";
        }

        for (int i=0; i<recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                resultSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
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

    // Este método é chamado pelo EventTrigger no item da UI quando você toca nele para começar a arrastar
    public void OnMouseDownItem(Item item)
    {
        if (currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
            
            // Já atualiza a posição imediatamente para o item não "pular" visualmente
            customCursor.transform.position = currentInputPosition;
        }
    }
}