using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class MiraEAtirarCursor : MonoBehaviour
{
    [Header("UI")]
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

    [Header("Animações")]
    public Animator animator;
    public string animIdle = "Idle";       // Nome da animação Idle
    public string animMirando = "mirando"; // Nome da animação de mira (opcional)
    public string anim;

    [Header("Animator Parameters")]
    public string boolCarregando = "IsCarregando";  // Boolean do Animator
    public string triggerAtirar = "Atirar";         // Trigger do Animator

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("cobra"))
        {
            inimigoAlvo = hit.collider.GetComponent<Inimigo>();

            // Segura botão direito para mirar
            if (Input.GetMouseButton(1))
            {
                if (!slider.gameObject.activeSelf)
                    slider.gameObject.SetActive(true);

                // Áudio de recarregar (uma vez só)
                if (!somCarregadoTocado)
                {
                    RuntimeManager.PlayOneShot(somCarregar, transform.position);
                    somCarregadoTocado = true;
                }

                estaCarregando = true;

                // Toca animação de mira/carregamento
                if (animator != null)
                    animator.SetBool(boolCarregando, true);
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

            // Botão esquerdo dispara o ataque
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
        if (animator != null)
            animator.SetTrigger(triggerAtirar); // dispara animação de ataque

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

        // Desliga animação de mira/carregamento
        if (animator != null)
            animator.SetBool(boolCarregando, false);
    }
}
