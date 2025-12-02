using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class HUD_Coleta : MonoBehaviour
{
    public TextMeshProUGUI textoComida;
    public TextMeshProUGUI textoOnca;
    public TextMeshProUGUI textoGuarana;

    [HideInInspector] public int comidaColetada = 0;
    [HideInInspector] public int oncaColetada = 0;
    [HideInInspector] public int guaranaColetado = 0;

    public string nomeDaProximaCena; // <--- coloque o nome da cena aqui no inspector

    public void AtualizarHUD()
    {
        if (textoComida != null)
            textoComida.text = comidaColetada + "/5";
        if (textoOnca != null)
            textoOnca.text = oncaColetada + "/0";
        if (textoGuarana != null)
            textoGuarana.text = guaranaColetado + "/3";

        VerificarConclusao(); // <--- chama a verificação
    }

    private void VerificarConclusao()
    {
        if (comidaColetada >= 5 && guaranaColetado >= 3)
        {
            SceneManager.LoadScene(nomeDaProximaCena);
        }
    }
}
