using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class MiraEAtirarCursor : MonoBehaviour
{
    public Slider slider;
    public float tempoCarregamento = 3f;
    public TextMeshProUGUI textoInimigos;
    public int inimigosDerrotados = 0;
    public int totalInimigos = 3;

    private bool estaCarregando = false;
    private float tempoAtual = 0f;
    private bool podeAtirar = false;
    private Inimigo inimigoAlvo;

    [Header("FMOD")]
    [SerializeField] private EventReference somCarregar;
    [SerializeField] private EventReference somAtirar;

    private bool somCarregadoTocado = false;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("cobra"))
        {
            inimigoAlvo = hit.collider.GetComponent<Inimigo>();

            if (Input.GetMouseButton(1))
            {
                if (!slider.gameObject.activeSelf)
                {
                    slider.gameObject.SetActive(true);
                }

                // AQUI O AUDIO DE RECARREGAR
                if (!somCarregadoTocado)
                {
                    RuntimeManager.PlayOneShot(somCarregar, transform.position);
                    somCarregadoTocado = true;
                }

                estaCarregando = true;
            }
            else
            {
                ResetarCarregamento();
            }

            if (estaCarregando)
            {
                tempoAtual += Time.deltaTime;
                slider.value = Mathf.Clamp01(tempoAtual / tempoCarregamento);
                podeAtirar = slider.value >= 0.9f;
            }

            if (Input.GetMouseButtonDown(0) && podeAtirar)
            {
                Atirar();
            }
        }
        else
        {
            inimigoAlvo = null;
            ResetarCarregamento();
        }
    }

    void Atirar()
    {
        if (inimigoAlvo != null)
        {
            inimigoAlvo.TomarDano();
            RuntimeManager.PlayOneShot(somAtirar, transform.position);
            inimigosDerrotados++;
            textoInimigos.text = inimigosDerrotados + " / " + totalInimigos;
            ResetarCarregamento();
        }
    }

    void ResetarCarregamento()
    {
        estaCarregando = false;
        tempoAtual = 0f;
        slider.value = 0f;
        slider.gameObject.SetActive(false);
        podeAtirar = false;
        somCarregadoTocado = false;
    }
}
