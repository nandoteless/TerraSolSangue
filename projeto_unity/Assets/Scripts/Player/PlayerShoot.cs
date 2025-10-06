using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Animator animator; // arraste o Animator do Player aqui no Inspector
    private GameObject targetCobra; // referência da cobra que o player vai atirar

    // Chamado quando o jogador decide atirar na cobra
    public void AimAndShootAt(GameObject cobra)
    {
        targetCobra = cobra;

        // Toca diretamente a animação "Atirando", sem transição
        animator.Play("Atirando", 0, 0f);
    }

    // Essa função será chamada automaticamente pelo Animation Event no momento do disparo
    public void OnFire()
    {
        if (targetCobra != null)
        {
            var coleta = targetCobra.GetComponent<ColetaItem>();
            if (coleta != null)
            {
                coleta.Coleta(); // destrói o objeto e adiciona ao inventário
            }
            else
            {
                Debug.LogWarning("A cobra não possui o script ColetaItem!");
            }
        }
    }
}
