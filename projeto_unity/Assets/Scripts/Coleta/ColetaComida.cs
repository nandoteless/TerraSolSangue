using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColetaComida : MonoBehaviour
{
    private HUD_Coleta hud;
    private DesbloqueioDeFase desbloqueio;
    public float delayColeta = 0.3f; // tempo antes de poder coletar

    private bool podeColetar = false;

    void Start()
    {
        hud = FindObjectOfType<HUD_Coleta>();
        desbloqueio = FindObjectOfType<DesbloqueioDeFase>();
        Invoke(nameof(HabilitarColeta), delayColeta);
    }

    void HabilitarColeta() => podeColetar = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!podeColetar) return;

        if (other.CompareTag("Player"))
        {
            // Atualiza HUD
            if (hud != null)
            {
                hud.comidaColetada++;
                hud.AtualizarHUD();
            }

            // Atualiza desbloqueio de fase
            if (desbloqueio != null)
            {
                desbloqueio.AdicionarComida();
            }

            // Remove o objeto
            Destroy(gameObject);
        }
    }
}


