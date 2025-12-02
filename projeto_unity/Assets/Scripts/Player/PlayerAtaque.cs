using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAtaque : MonoBehaviour
{
    public Transform pontoDeAtaque;
    public float raioDeAtaque = 0.5f;
    public float dano = 20f;

    public float tempoEntreAtaques = 0.5f;
    private float tempoProximoAtaque = 0f;

    public InputActionReference comandoAtacar;

    private Animator anim;

    // ---- ADIÇÃO PARA O SOM ----
    public AudioClip somLanca; 
    private AudioSource audioSource;
    // ---------------------------

    private void Awake()
    {
        anim = GetComponent<Animator>();

        // ---- Pega ou cria um AudioSource automaticamente ----
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnEnable()
    {
        comandoAtacar.action.Enable();
        comandoAtacar.action.performed += RealizarAtaque;
    }

    private void OnDisable()
    {
        comandoAtacar.action.performed -= RealizarAtaque;
        comandoAtacar.action.Disable();
    }

    private void RealizarAtaque(InputAction.CallbackContext context)
    {
        if (Time.time < tempoProximoAtaque)
            return;

        tempoProximoAtaque = Time.time + tempoEntreAtaques;

        anim.SetTrigger("Atacando");

        // ---- SOM DA LANÇA TOCANDO AQUI ----
        if (somLanca != null)
            audioSource.PlayOneShot(somLanca);
        // -----------------------------------

        Collider2D[] objetosAtingidos = Physics2D.OverlapCircleAll(pontoDeAtaque.position, raioDeAtaque);

        foreach (Collider2D objeto in objetosAtingidos)
        {
            if (objeto.CompareTag("inimigo"))
            {
                BartolomeuVida vidaScript = objeto.GetComponent<BartolomeuVida>();

                if (vidaScript != null)
                {
                    vidaScript.ReduzirVida(dano);
                    Debug.Log("Acertou o inimigo!");
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pontoDeAtaque == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pontoDeAtaque.position, raioDeAtaque);
    }
}
