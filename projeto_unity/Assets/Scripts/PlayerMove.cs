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

    public Transform npc;

    DialogueSystem dialogueSystem;


    private void Awake()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }
    // Update is called once per frame
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
        if (context.performed)
        {
            if(Mathf.Abs(transform.position.x - npc.position.x) < 2.0f)
            {
                dialogueSystem.StartDialogue();
            }
        }
    }
}



