using System.Collections;
using UnityEngine;

public class BartolomeuIA : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 2f;
    public float distanciaParaAtacar = 1.2f;

    [Header("Ataque")]
    public float dano = 20f;
    public float tempoEntreAtaques = 1f;

    [Header("Flip")]
    [Tooltip("Marque true se o sprite padrão (escala X positiva) estiver olhando para a direita.")]
    public bool defaultFacingRight = true;

    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;

    private bool podeAtacar = true;
    private Vector3 escalaOriginal;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        escalaOriginal = transform.localScale;

        if (player == null)
            Debug.LogWarning("BartolomeuIA: Nenhum objeto com tag 'Player' foi encontrado!");
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        // ---- ATAQUE POR TECLA J ----
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Ataque");
        }
        // ------------------------------

        // Flip do inimigo
        FlipInimigo();

        // Seguir jogador
        if (distancia > distanciaParaAtacar)
        {
            Movimentar();
        }
        else
        {
            rb.velocity = Vector2.zero;

            // Desativa animação de andar
            if (anim != null)
                anim.SetBool("andando", false);

            // Ataque automático
            if (podeAtacar)
                StartCoroutine(Atacar());
        }
    }

    void FlipInimigo()
    {
        int direçãoRelativa = player.position.x > transform.position.x ? 1 : -1;
        int multiplicador = defaultFacingRight ? direçãoRelativa : -direçãoRelativa;

        float novoX = Mathf.Abs(escalaOriginal.x) * multiplicador;
        transform.localScale = new Vector3(novoX, escalaOriginal.y, escalaOriginal.z);
    }

    void Movimentar()
    {
        if (anim != null)
            anim.SetBool("andando", true);

        Vector2 direcao = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direcao.x * velocidade, rb.velocity.y);
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;

        if (anim != null)
            anim.SetTrigger("Ataque");

        // espera o momento real do golpe
        yield return new WaitForSeconds(0.25f);

        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia <= distanciaParaAtacar + 0.2f)
        {
            PlayerVida vida = player.GetComponent<PlayerVida>();
            vida?.ReduzirVida(dano);
        }

        yield return new WaitForSeconds(tempoEntreAtaques);
        podeAtacar = true;
    }
}

