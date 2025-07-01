using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PigMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionInterval = 2f;
    private Vector2 moveDirection;
    private float directionTimer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;

        directionTimer -= Time.fixedDeltaTime;
        if (directionTimer <= 0)
        {
            ChooseNewDirection();
        }
    }

    void ChooseNewDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        directionTimer = Random.Range(1f, changeDirectionInterval);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "FLIP")
        {
            FlipSprite();
        }
        else
        {
            // Ao colidir com a parede do chiqueiro, muda de direção
            ChooseNewDirection();
        }
    }

    void FlipSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}