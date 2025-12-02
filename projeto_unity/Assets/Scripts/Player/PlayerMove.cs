using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using FMODUnity;




public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 mov;
    [SerializeField] Animator anim;
    [SerializeField] private PlayerSounds playerSounds;
    [SerializeField] private GameObject canvasCraftTable;
    private bool colidiuCraftTable;
    public Transform npc = null;            
    private DialogueSystem dialogueSystem;


    private void Awake()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        colidiuCraftTable = false;
    }


    void Update()
    {
        anim.SetFloat("Horizontal", mov.x);
        anim.SetFloat("Vertical", mov.y);
        anim.SetFloat("Speed", mov.sqrMagnitude);
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * speed * Time.fixedUnscaledDeltaTime);
    }


    private void PlayFootstep()
    {
        playerSounds.PlayFootsteps();
    }


    public void MoveEvent(InputAction.CallbackContext context)
    {
        mov = context.ReadValue<Vector2>();
    }


    public void InteractEvent(InputAction.CallbackContext context)
    {
        if (npc != null)
        {
            dialogueSystem.StartDialogue();
        }
       
        // DMVS Verifica se já pode realizar o CRAFTING
        if (GameManager.instancia.objetivoFaseConcluido)
        {
            // DMVS somente vai funcionar se canvasCraftTable for preenchido na Inspector, ou seja, somente na Fase3Pt2
            if (colidiuCraftTable && canvasCraftTable != null && !canvasCraftTable.activeSelf)
            {
                canvasCraftTable.SetActive(true);
                return;
            }
            else if (canvasCraftTable != null && canvasCraftTable.activeSelf)
            {
                canvasCraftTable.SetActive(false);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CraftTable"))
        {
            colidiuCraftTable = true;
           
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        colidiuCraftTable = false;
    }
   


    // Detecta automaticamente o NPC ao entrar no trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            npc = collision.transform;
            Debug.Log("NPC detectado: " + npc.name);
        }
       
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            npc = null;
        }
       
    }
    
   
}

