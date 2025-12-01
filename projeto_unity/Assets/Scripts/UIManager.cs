using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // 🔢 Referências na HUD
    [Header("Contadores de Itens")]
    public TextMeshProUGUI pauBrasilCounterText;
    public TextMeshProUGUI cipoCounterText;   // ✅ novo
    public TextMeshProUGUI pedraCounterText;  // ✅ novo

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // 📌 Atualiza os textos da HUD com os valores coletados e os valores necessários
    public void UpdateItemCounts(
        Dictionary<ColetaItem.TipoItem, int> collectedCounts,
        Dictionary<ColetaItem.TipoItem, int> requiredCounts)
    {
        Debug.Log("UpdateItemCounts");
        // --- PAU BRASIL ---
        if (pauBrasilCounterText != null &&
            collectedCounts.ContainsKey(ColetaItem.TipoItem.PauBrasil))
        {
            pauBrasilCounterText.text =
                collectedCounts[ColetaItem.TipoItem.PauBrasil] + "/" +
                requiredCounts[ColetaItem.TipoItem.PauBrasil];
        }

        // --- CIPÓ ---
        if (cipoCounterText != null &&
            collectedCounts.ContainsKey(ColetaItem.TipoItem.Cipo))
        {
            Debug.Log("CIPÓ");
            cipoCounterText.text =
                collectedCounts[ColetaItem.TipoItem.Cipo] + "/" +
                requiredCounts[ColetaItem.TipoItem.Cipo];
        }

        // --- PEDRA ---
        if (pedraCounterText != null &&
            collectedCounts.ContainsKey(ColetaItem.TipoItem.Pedra))
        {
            pedraCounterText.text =
                collectedCounts[ColetaItem.TipoItem.Pedra] + "/" +
                requiredCounts[ColetaItem.TipoItem.Pedra];
        }
    }

    void Start()
    {
        if (GameManager.instancia != null)
        {
            UpdateItemCounts(
                GameManager.instancia.itensColetados,
                GameManager.instancia.totalItensPorTipo
            );
        }
        else
        {
            UpdateItemCounts(new Dictionary<ColetaItem.TipoItem, int>(),
                             new Dictionary<ColetaItem.TipoItem, int>());
        }
    }
}
