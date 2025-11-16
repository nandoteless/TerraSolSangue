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
    private int HorizontalHash = Animator.StringToHash("Horizontal");
    private int VerticalHash = Animator.StringToHash("Vertical");

    void Update()
    {

        float speedValue = mov.sqrMagnitude;
        if (speedValue < 0.01f) speedValue = 0f;

        anim.SetFloat(HorizontalHash, mov.x);
        anim.SetFloat(VerticalHash, mov.y);
        anim.SetFloat("Speed", speedValue);
    }



    void FixedUpdate()
    {
        // Movimento real aplicado no Rigidbody2D
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

}

