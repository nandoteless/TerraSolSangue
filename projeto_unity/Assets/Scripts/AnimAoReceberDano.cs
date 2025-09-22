using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimAoReceberDano : MonoBehaviour
{
    public string animParametro = "RecebeuDano"; // bool no Animator
    public float duracaoAnim = 0.5f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogWarning("Animator não encontrado no Player!");
        }
    }

    // Função para ser chamada sempre que o Player recebe dano
    public void AcionarAnimacao()
    {
        if (anim != null)
        {
            anim.SetBool(animParametro, true);
            Invoke("PararAnimacao", duracaoAnim);
        }
    }

    void PararAnimacao()
    {
        if (anim != null)
        {
            anim.SetBool(animParametro, false);
        }
    }
}
