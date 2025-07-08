using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // Referências aos objetos de texto para cada tipo de item
    public TextMeshProUGUI pauBrasilCounterText;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método atualizado para receber as contagens coletadas e os totais requeridos
    public void UpdateItemCounts(Dictionary<ColetaItem.TipoItem, int> collectedCounts, Dictionary<ColetaItem.TipoItem, int> requiredCounts)
    {
        // Atualiza o texto para cada tipo de item individual, incluindo o total requerido
        if (pauBrasilCounterText != null && collectedCounts.ContainsKey(ColetaItem.TipoItem.PauBrasil) && requiredCounts.ContainsKey(ColetaItem.TipoItem.PauBrasil))
        {
            pauBrasilCounterText.text = collectedCounts[ColetaItem.TipoItem.PauBrasil].ToString() + "/" + requiredCounts[ColetaItem.TipoItem.PauBrasil].ToString();
        }
    }

    void Start()
    {
        // Garante que a HUD seja inicializada corretamente ao iniciar a cena
        if (GameManager.instancia != null)
        {
            UpdateItemCounts(GameManager.instancia.itensColetados, GameManager.instancia.totalItensPorTipo);
        }
        else
        {
            // Caso o GameManager ainda não esteja pronto, inicialize com zeros
            UpdateItemCounts(new Dictionary<ColetaItem.TipoItem, int>(), new Dictionary<ColetaItem.TipoItem, int>());
        }
    }
}