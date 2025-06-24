using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Button botaoIniciar;
    public Button botaoJogar;
    public Button botaoHistoria;
    public Button botaoMenu;
    public Button botaoInstrucao;


    void Start()
    {
        // Adiciona o listener ao bot�o se estiver atribu�do no Inspector
        if (botaoIniciar != null)
        {
            botaoIniciar.onClick.AddListener(CarregarMenu);
        }

         if (botaoJogar != null)
        {
            botaoJogar.onClick.AddListener(CarregarFase1);
        }

          if (botaoHistoria != null)
        {
            botaoHistoria.onClick.AddListener(CarregarHistoria);
        }

         if (botaoMenu != null)
        {
            botaoMenu.onClick.AddListener(CarregarVoltar);
        }

         if (botaoInstrucao != null)
        {
            botaoInstrucao.onClick.AddListener(CarregarInstrucao);
        }
    }

    public void CarregarMenu()
    {
        // Carrega a cena chamada "Menu"
        SceneManager.LoadScene("Menu");
    }

      public void CarregarFase1()
    {
        SceneManager.LoadScene("Cuts1");
    }

     public void CarregarHistoria()
    {
        SceneManager.LoadScene("Historia");
    }

     public void CarregarVoltar()
    {
        SceneManager.LoadScene("Menu");
    }

      public void CarregarInstrucao()
    {
        SceneManager.LoadScene("Instrucao");
    }
}