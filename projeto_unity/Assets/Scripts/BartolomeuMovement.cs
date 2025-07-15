using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    mov.x = InputManager.instancia.movement.x;
    mov.y = InputManager.instancia.movement.y;

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

