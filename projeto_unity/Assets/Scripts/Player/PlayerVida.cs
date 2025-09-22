using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // ← Importante para usar TextMeshProUGUI



using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerVida : MonoBehaviour
{
    public Slider vidaSlider;
    public float vidaMaxima = 100f;
    private float vidaAtual;

    [Header("Guaraná")]
    public TextMeshProUGUI guaranaTMP;
    private int guaranaContagem = 0;
    private int guaranaTotal = 5;

    [Header("Feedback de Dano")]
    public GameObject efeitoDano;   // arraste o GameObject que será ativado (ex: tela vermelha, sprite, etc)
    public float tempoAtivo = 0.5f; // tempo que o efeito fica ativo

    void Start()
    {
        vidaAtual = vidaMaxima;

        if (vidaSlider != null)
        {
            vidaSlider.maxValue = vidaMaxima;
            vidaSlider.value = vidaAtual;
        }
        else
        {
            Debug.LogWarning("Slider de vida não atribuído!");
        }

        AtualizarTextoGuarana();

        // Garante que o efeito começa desativado
        if (efeitoDano != null)
            efeitoDano.SetActive(false);
    }

    public void ReduzirVida(float quantidade)
    {
        vidaAtual -= quantidade;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        if (vidaSlider != null)
            vidaSlider.value = vidaAtual;

        // Ativa o feedback de dano
        if (efeitoDano != null)
        {
            StopAllCoroutines(); // evita bug se tomar vários danos seguidos
            StartCoroutine(AtivarEfeitoDano());
        }

        if (vidaAtual <= 0)
        {
            Debug.Log("GAME OVER");
            ReiniciarCena();
        }
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

        if (vidaSlider != null)
            vidaSlider.value = vidaAtual;
    }

    void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cobra") || other.CompareTag("onca"))
        {
            Debug.Log("Player levou dano de: " + other.tag);
            ReduzirVida(25f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("guarana"))
        {
            Debug.Log("Colidiu com o arbusto de guaraná!");
            AumentarVida(25f);
            AdicionarGuarana();
            Destroy(collision.gameObject);
        }
    }

    void AdicionarGuarana()
    {
        if (guaranaContagem < guaranaTotal)
        {
            guaranaContagem++;
            AtualizarTextoGuarana();
        }
    }

    void AtualizarTextoGuarana()
    {
        if (guaranaTMP != null)
        {
            guaranaTMP.text = guaranaContagem + " / " + guaranaTotal;
        }
    }
}
