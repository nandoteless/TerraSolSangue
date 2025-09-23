using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_Coleta : MonoBehaviour
{
    public TextMeshProUGUI textoComida;
    public TextMeshProUGUI textoOnca;
    public TextMeshProUGUI textoGuarana;

    [HideInInspector] public int comidaColetada = 0;
    [HideInInspector] public int oncaColetada = 0;
    [HideInInspector] public int guaranaColetado = 0;

    public void AtualizarHUD()
    {
        if (textoComida != null)
            textoComida.text = "Comidas: " + comidaColetada + "/4";
        if (textoOnca != null)
            textoOnca.text = "Onças: " + oncaColetada + "/3";
        if (textoGuarana != null)
            textoGuarana.text = "Guaranás: " + guaranaColetado + "/3";
    }
}
