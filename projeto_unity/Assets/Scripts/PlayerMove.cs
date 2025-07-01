using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] Vector2 mov;

    [SerializeField] Animator anim;

    public Transform npc;

    DialogueSystem dialogueSystem;


    private void Awake ()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", mov.x);
        anim.SetFloat("Vertical", mov.y);
        anim.SetFloat("Speed", mov.sqrMagnitude);

        mov.Normalize();


         if(Mathf.Abs(transform.position.x - npc.position.x) < 2.0f) {
            if(Input.GetKeyDown(KeyCode.E)) {
                dialogueSystem.Next();
            }

           
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * speed * Time.fixedUnscaledDeltaTime);
    }

}
