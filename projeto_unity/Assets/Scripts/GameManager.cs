using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public UIManager uiManager;

    [System.Serializable]
    public struct ObjetivoItem
    {
        public ColetaItem.TipoItem tipoItem;
        public int total;
    }

    public List<ObjetivoItem> itemGoals;
    public Dictionary<ColetaItem.TipoItem, int> itensColetados;
    public Dictionary<ColetaItem.TipoItem, int> totalItensPorTipo;
    public bool objetivoFaseConcluido;

    [Header("Referência da Vara (para desbloqueio)")]
    public GameObject varaParaDesbloquear;

    private SceneLoaderWithTimer sceneLoaderWithTimer = null;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        objetivoFaseConcluido = true;
        
        itensColetados = new Dictionary<ColetaItem.TipoItem, int>();
        foreach (ColetaItem.TipoItem tipo in System.Enum.GetValues(typeof(ColetaItem.TipoItem)))
        {
            itensColetados.Add(tipo, 0);
        }

        totalItensPorTipo = itemGoals.ToDictionary(obj => obj.tipoItem, obj => obj.total);

        foreach (ColetaItem.TipoItem tipo in System.Enum.GetValues(typeof(ColetaItem.TipoItem)))
        {
            if (!totalItensPorTipo.ContainsKey(tipo))
            {
                totalItensPorTipo.Add(tipo, 0);
            }
        }
    }

    void Start()
    {
        sceneLoaderWithTimer = GetComponent<SceneLoaderWithTimer>();
    }

    public void ColetaItem(ColetaItem.TipoItem tipo, int valor)
    {
        if (itensColetados.ContainsKey(tipo))
        {
            itensColetados[tipo] += valor;
        }
        else
        {
            itensColetados.Add(tipo, valor);
        }

        if (uiManager != null)
        {
            uiManager.UpdateItemCounts(itensColetados, totalItensPorTipo);
        }

        if (totalItensPorTipo.ContainsKey(tipo) && itensColetados[tipo] >= totalItensPorTipo[tipo])
        {
            Debug.Log(tipo.ToString() + " totalmente coletado!");
        }

        bool objetivosConcluidos = true;
        foreach (var objetivo in totalItensPorTipo)
        {
            if (itensColetados[objetivo.Key] < objetivo.Value)
            {
                objetivosConcluidos = false;
                break;
            }
        }

        if (objetivosConcluidos && totalItensPorTipo.Any(objetivo => objetivo.Value > 0))
        {
            Debug.Log("Todos os objetivos de coleta alcançados! Vitória!");
            objetivoFaseConcluido = true;

            if (varaParaDesbloquear != null)
            {
                varaParaDesbloquear.SetActive(true);

                Collider2D col = varaParaDesbloquear.GetComponent<Collider2D>();
                if (col != null)
                    col.enabled = true;
            }
        }
    }

    public int GetItemCount(ColetaItem.TipoItem tipo)
    {
        if (itensColetados.ContainsKey(tipo))
        {
            return itensColetados[tipo];
        }
        return 0;
    }

    public int GetTotalPorTipo(ColetaItem.TipoItem tipo)
    {
        if (totalItensPorTipo.ContainsKey(tipo))
        {
            return totalItensPorTipo[tipo];
        }
        return 0;
    }

    public bool PossoColetarItem(ColetaItem.TipoItem tipo)
    {
        if (itensColetados[tipo] >= totalItensPorTipo[tipo])
            return false;
        return true;
    }

    public bool ObjetivosConcluidos()
    {
        foreach (var objetivo in totalItensPorTipo)
        {
            if (itensColetados[objetivo.Key] < objetivo.Value)
            {
                return false;
            }
        }

        return totalItensPorTipo.Any(objetivo => objetivo.Value > 0);
    }

    public void LoadNextScene() 
    {
        if (sceneLoaderWithTimer != null)
        {
            sceneLoaderWithTimer.StartNewScene();
        }
    }
}
