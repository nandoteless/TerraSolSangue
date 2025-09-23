using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ColetaBife : MonoBehaviour
{
    public string mensagemColeta = "Onça coletada!";
    public int pontos = 10;
    public float delayColeta = 0.5f;

    private bool podeColetar = false;
    private HUD_Coleta hud;
    private DesbloqueioDeFase desbloqueio;

    void Start()
    {
        hud = FindObjectOfType<HUD_Coleta>();
        desbloqueio = FindObjectOfType<DesbloqueioDeFase>();

        Invoke(nameof(HabilitarColeta), delayColeta);
    }

    void HabilitarColeta() => podeColetar = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!podeColetar) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log(mensagemColeta);

            // Atualiza HUD
            if (hud != null)
            {
                hud.oncaColetada++;
                hud.AtualizarHUD();
            }

            // Atualiza desbloqueio de fase
            if (desbloqueio != null)
            {
                desbloqueio.AdicionarInimigoDerrotado(); // ou criar AdicionarOncaColetada()
            }

            Destroy(gameObject);
        }
    }
}
