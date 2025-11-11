using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using FMODUnity;

public class PlayerMove : MonoBehaviour
{
    [Header("FMOD")]
    [SerializeField] private EventReference som;
    [SerializeField] float speed;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] Vector2 mov;

    [SerializeField] Animator anim;
    [SerializeField] private PlayerSounds playerSounds;
    [SerializeField] private GameObject canvasCraftTable;
    private bool colidiuCraftTable;

    private Transform npc;            // <-- NÃO precisa mais arrastar no Inspector
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
        if (!context.performed) return;
        
        if (colidiuCraftTable && !canvasCraftTable.activeSelf)
        {
            canvasCraftTable.SetActive(true);
            return;
        } 
        else if (canvasCraftTable.activeSelf)
        {
            canvasCraftTable.SetActive(false);
        }
        if (npc == null)
        {
            Debug.Log("Nenhum NPC perto para interagir.");
            return;
        }

        // Agora usa distância real (360°), não depende só do eixo X
        if (Vector2.Distance(transform.position, npc.position) < 2f)
        {
            dialogueSystem.StartDialogue();
        }
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            npc = null;
            Debug.Log("NPC saiu da área de interação.");
        }
        
    }
}
