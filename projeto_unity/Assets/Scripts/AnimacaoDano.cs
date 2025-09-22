using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimacaoDano : MonoBehaviour
{
    public Animator animator;         // Animator do Player
    public string parametroAnim = "RecebeuDano"; // Nome do bool no Animator
    public float duracaoAnim = 0.5f;  // tempo que a animação fica ativa

    // Chame esta função sempre que o Player receber dano
    public void AcionarAnimacao()
    {
        if (animator != null)
        {
            animator.SetBool(parametroAnim, true);
            Invoke(nameof(PararAnimacao), duracaoAnim);
        }
    }

    private void PararAnimacao()
    {
        if (animator != null)
        {
            animator.SetBool(parametroAnim, false);
        }
    }
}
