using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesbloqueioDeFase : MonoBehaviour
{
    [Header("Condições para passar de fase")]
    public int comidasNecessarias = 5;      // quantidade de comida que precisa coletar
    public int guaranasNecessarios = 5;     // quantidade de guaranás que precisa coletar
    public int inimigosNecessarios = 3;     // quantidade de inimigos que precisa derrotar

    [Header("Nome da próxima fase")]
    public string proximaFase;

    // Contadores internos
    private int comidasColetadas = 0;
    private int guaranasColetados = 0;
    private int inimigosDerrotados = 0;

    void Update()
    {
        // Verifica se todas as condições foram cumpridas
        if (comidasColetadas >= comidasNecessarias &&
            guaranasColetados >= guaranasNecessarios &&
            inimigosDerrotados >= inimigosNecessarios)
        {
            DesbloquearProximaFase();
        }
    }

    // Chamado quando coleta comida
    public void AdicionarComida()
    {
        comidasColetadas++;
        Debug.Log("Comidas coletadas: " + comidasColetadas + " / " + comidasNecessarias);
    }

    // Chamado quando coleta um guaraná
    public void AdicionarGuarana()
    {
        guaranasColetados++;
        Debug.Log("Guaranás coletados: " + guaranasColetados + " / " + guaranasNecessarios);
    }

    // Chamado quando derrota um inimigo
    public void AdicionarInimigoDerrotado()
    {
        inimigosDerrotados++;
        Debug.Log("Inimigos derrotados: " + inimigosDerrotados + " / " + inimigosNecessarios);
    }

    private void DesbloquearProximaFase()
    {
        Debug.Log("Todas as condições cumpridas! Carregando a próxima fase...");
        SceneManager.LoadScene(proximaFase);
    }
}
