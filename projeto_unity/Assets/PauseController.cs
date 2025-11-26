using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
   public GameObject painelPause; // arraste o painel aqui no Inspector
    private bool jogoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (jogoPausado)
            {
                Despausar();
            }
            else
            {
                Pausar();
            }
        }
    }

    void Pausar()
    {
        Time.timeScale = 0f; // pausa o jogo
        painelPause.SetActive(true); // mostra o painel
        jogoPausado = true;
    }

    void Despausar()
    {
        Time.timeScale = 1f; // volta ao normal
        painelPause.SetActive(false); // esconde o painel
        jogoPausado = false;
    }
}