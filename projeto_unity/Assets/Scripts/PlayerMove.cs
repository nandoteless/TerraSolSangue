using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] Vector2 mov;

    [SerializeField] Animator anim;

  

    // Update is called once per frame
    void Update()
    {
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", mov.x);
        anim.SetFloat("Vertical", mov.y);
        anim.SetFloat("Speed", mov.sqrMagnitude);

        mov.Normalize();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * speed * Time.fixedUnscaledDeltaTime);
    }

}
