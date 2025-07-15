using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;

public class CanvasController : MonoBehaviour
{
    [Header("FMOD")]
    [SerializeField] private EventReference som;

    public Button botaoIniciar;
    public Button botaoJogar;
    public Button botaoHistoria;
    public Button botaoMenu;
    public Button botaoInstrucao;

    void Start()
    {
        if (botaoIniciar != null)
            botaoIniciar.onClick.AddListener(CarregarMenu);

        if (botaoJogar != null)
            botaoJogar.onClick.AddListener(CarregarFase1);

        if (botaoHistoria != null)
            botaoHistoria.onClick.AddListener(CarregarHistoria);

        if (botaoMenu != null)
            botaoMenu.onClick.AddListener(CarregarVoltar);

        if (botaoInstrucao != null)
            botaoInstrucao.onClick.AddListener(CarregarInstrucao);
    }

    public void CarregarMenu()
    {
        RuntimeManager.PlayOneShot(som, transform.position);
        SceneManager.LoadScene("Menu");
    }

    public void CarregarFase1()
    {
        RuntimeManager.PlayOneShot(som, transform.position);
        SceneManager.LoadScene("Cuts1");
    }

    public void CarregarHistoria()
    {
        RuntimeManager.PlayOneShot(som, transform.position);
        SceneManager.LoadScene("Historia");
    }

    public void CarregarVoltar()
    {
        RuntimeManager.PlayOneShot(som, transform.position);
        SceneManager.LoadScene("Menu");
    }

    public void CarregarInstrucao()
    {
        RuntimeManager.PlayOneShot(som, transform.position);
        SceneManager.LoadScene("Instrucao");
    }
}
