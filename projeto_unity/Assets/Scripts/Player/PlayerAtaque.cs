using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAtaque : MonoBehaviour
{
    [Header("Configurações de Combate")]
    public Transform pontoDeAtaque;
    public float raioDeAtaque = 0.5f;
    public float dano = 20f;

    [Header("Cooldown (Tempo de Recarga)")]
    public float tempoEntreAtaques = 0.5f; // Tempo em segundos entre cada ataque
    private float tempoProximoAtaque = 0f; // Armazena o momento em que poderá atacar de novo

    [Header("Configurações de Input")]
    public InputActionReference comandoAtacar;

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
        // 1. Verifica se o cooldown está ativo
        // Se o tempo atual for MENOR que o tempo permitido, cancela o ataque
        if (Time.time < tempoProximoAtaque)
        {
            return;
        }

        // 2. Define o próximo momento em que o ataque será permitido
        tempoProximoAtaque = Time.time + tempoEntreAtaques;

        // ---------------- Lógica de Dano Abaixo ---------------- //

        // 3. Detecta TUDO que estiver dentro do círculo
        Collider2D[] objetosAtingidos = Physics2D.OverlapCircleAll(pontoDeAtaque.position, raioDeAtaque);

        foreach (Collider2D objeto in objetosAtingidos)
        {
            // 4. Filtra pela TAG
            if (objeto.CompareTag("inimigo"))
            {
                BartolomeuVida vidaScript = objeto.GetComponent<BartolomeuVida>();

                if (vidaScript != null)
                {
                    vidaScript.ReduzirVida(dano);
                    Debug.Log("Acertou o inimigo pela Tag! Dano aplicado.");
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