using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PlayerVida : MonoBehaviour
{
    [Header("Vida")]
    public Slider vidaSlider;
    public float vidaMaxima = 100f;
    private float vidaAtual;

    [Header("Guaraná")]
    public TextMeshProUGUI guaranaTMP;
    private int guaranaContagem = 0;
    public int guaranaTotal = 5;

    [Header("Feedback de Dano")]
    public GameObject efeitoDano;   
    public float tempoAtivo = 0.5f;

    private HUD_Coleta hud;
    private DesbloqueioDeFase desbloqueio;

    void Start()
    {
        vidaAtual = vidaMaxima;
        if (vidaSlider != null) vidaSlider.maxValue = vidaMaxima;
        if (vidaSlider != null) vidaSlider.value = vidaAtual;

        if (efeitoDano != null) efeitoDano.SetActive(false);
        AtualizarTextoGuarana();

        // Referências
        hud = FindObjectOfType<HUD_Coleta>();
        desbloqueio = FindObjectOfType<DesbloqueioDeFase>();
    }

    public void ReduzirVida(float quantidade)
    {
        vidaAtual -= quantidade;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);
        if (vidaSlider != null) vidaSlider.value = vidaAtual;

        if (efeitoDano != null)
        {
            StopAllCoroutines();
            StartCoroutine(AtivarEfeitoDano());
        }

        if (vidaAtual <= 0) ReiniciarCena();
    }

    private IEnumerator AtivarEfeitoDano()
    {
        efeitoDano.SetActive(true);
        yield return new WaitForSeconds(tempoAtivo);
        efeitoDano.SetActive(false);
    }

    public void AumentarVida(float quantidade)
    {
        vidaAtual += quantidade;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);
        if (vidaSlider != null) vidaSlider.value = vidaAtual;
    }

    void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cobra") || other.CompareTag("onca"))
        {
            ReduzirVida(25f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("guarana"))
        {
            AumentarVida(25f);
            AdicionarGuarana();

            // Atualiza desbloqueio de fase
            if (desbloqueio != null) desbloqueio.AdicionarGuarana();

            Destroy(collision.gameObject);
        }
    }

    void AdicionarGuarana()
    {
        if (guaranaContagem < guaranaTotal)
        {
            guaranaContagem++;
            AtualizarTextoGuarana();

            // Atualiza HUD
            if (hud != null)
            {
                hud.guaranaColetado = guaranaContagem;
                hud.AtualizarHUD();
            }
        }
    }

    void AtualizarTextoGuarana()
    {
        if (guaranaTMP != null)
            guaranaTMP.text = guaranaContagem + " / " + guaranaTotal;
    }
}
