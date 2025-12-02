using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// 1. IMPORTANTE: Adicionamos esta linha para gerenciar cenas
using UnityEngine.SceneManagement; 


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

    [Header("Som de Mordida")]
    public AudioSource somMordida;

    private HUD_Coleta hud;
    private DesbloqueioDeFase desbloqueio;
    public string nomeDaFaseFinal = "FaseFinal";

    void Start()
    {
        vidaAtual = vidaMaxima;
        if (vidaSlider != null) vidaSlider.maxValue = vidaMaxima;
        if (vidaSlider != null) vidaSlider.value = vidaAtual;

        if (efeitoDano != null) efeitoDano.SetActive(false);
        AtualizarTextoGuarana();

        hud = FindObjectOfType<HUD_Coleta>();
        desbloqueio = FindObjectOfType<DesbloqueioDeFase>();
    }

    public void ReduzirVida(float quantidade)
    {
        vidaAtual -= quantidade;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima); // Garante que a vida não fique negativa (ex: -10)
        
        if (vidaSlider != null) vidaSlider.value = vidaAtual;

        // Ativa o efeito visual de dano se o jogador ainda estiver vivo
        if (vidaAtual > 0 && efeitoDano != null)
        {
            StopAllCoroutines();
            StartCoroutine(AtivarEfeitoDano());
        }

        // 2. Lógica de Morte e Reinício
        if (vidaAtual <= 0)
        {
            //carregar cena chamada endof;
        }
    }

    // Função dedicada para reiniciar a fase atual
    void ReiniciarCena()
    {
        // Pega o nome da cena que está ativa no momento e a carrega novamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator AtivarEfeitoDano()
    {
        efeitoDano.SetActive(true);

        if (somMordida != null)
            somMordida.Play();

        yield return new WaitForSeconds(tempoAtivo);
        efeitoDano.SetActive(false);
    }

    public void AumentarVida(float quantidade)
    {
        vidaAtual += quantidade;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);
        if (vidaSlider != null) vidaSlider.value = vidaAtual;
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

            if (desbloqueio != null)
                desbloqueio.AdicionarGuarana();

            Destroy(collision.gameObject);
        }
    }

    void AdicionarGuarana()
    {
        if (guaranaContagem < guaranaTotal)
        {
            guaranaContagem++;
            AtualizarTextoGuarana();

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