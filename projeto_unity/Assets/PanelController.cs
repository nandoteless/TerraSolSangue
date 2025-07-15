using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel;  // Arraste seu panel aqui no inspector

    private void Start()
    {
        // Exibe o painel quando a cena for carregada
        panel.SetActive(true);

        // Chama a função para esconder o painel após 5 segundos
        Invoke("HidePanel", 5f);
    }

    // Função para esconder o painel
    private void HidePanel()
    {
        panel.SetActive(false);
    }
}