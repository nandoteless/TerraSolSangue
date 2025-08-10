using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BartolomeuMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerSounds playerSounds;
    private Vector2 mov;

    void Update()
{
    if (InputManager.instancia == null) return;

    mov = InputManager.instancia.movement;
     Debug.Log("MOV: " + mov);

    if (mov != Vector2.zero)
        Debug.Log("Movendo: " + mov); // Veja isso no Console

    anim.SetFloat("Horizontal", mov.x);
    anim.SetFloat("Vertical", mov.y);
    anim.SetFloat("Speed", mov.sqrMagnitude);
}



    void FixedUpdate()
    {
        // Movimento real aplicado no Rigidbody2D
        rb.MovePosition(rb.position + mov * speed * Time.fixedUnscaledDeltaTime);
    }

    private void PlayFootstep()
    {
        playerSounds.PlayFootsteps(); // Certifique-se de que esse método está configurado corretamente
    }
}

