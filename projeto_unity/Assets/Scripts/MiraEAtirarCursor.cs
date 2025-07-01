using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiraEAtirarCursor : MonoBehaviour
{
      public Slider slider;              
    public float tempoCarregamento = 3f; // Tempo para carregar totalmente
    public TextMeshProUGUI textoInimigos; // Texto que vai mostrar a quantidade de inimigos derrotados
    public int inimigosDerrotados = 0;  // Contador de inimigos derrotados
    public int totalInimigos = 3;       // Número total de inimigos

    private bool estaCarregando = false; // Se o botão direito está pressionado
    private float tempoAtual = 0f;        // Tempo de carregamento
    private bool podeAtirar = false;     // Verifica se pode atirar (acima de 90%)
    private Inimigo inimigoAlvo;         // Armazena o inimigo que está sendo mirado

    void Update()
    {
        // Detecta se o cursor está em cima de um inimigo
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        // Verifica se o cursor está sobre um inimigo
        if (hit.collider != null && hit.collider.CompareTag("cobra"))
        {
            inimigoAlvo = hit.collider.GetComponent<Inimigo>();

            if (Input.GetMouseButton(1)) // Se o botão direito estiver pressionado
            {
                if (!slider.gameObject.activeSelf) // Só ativa o slider uma vez
                {
                    slider.gameObject.SetActive(true); // Ativa o slider ao pressionar o botão direito
                }
                estaCarregando = true;
            }
            else
            {
                ResetarCarregamento();
            }

            // Se estiver carregando, aumenta o slider
            if (estaCarregando)
            {
                tempoAtual += Time.deltaTime;
                slider.value = Mathf.Clamp01(tempoAtual / tempoCarregamento); // Controla o valor do slider

                podeAtirar = slider.value >= 0.9f;
            }

            // Quando o botão esquerdo for pressionado e o slider estiver 90% ou mais
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
        slider.gameObject.SetActive(false); // Desativa o slider após disparar ou soltar o botão
        podeAtirar = false;
    }
}