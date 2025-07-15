using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 2.0f;
    private Vector3 direction;

    void Start()
    {
        // Define a direção inicial do peixe (da esquerda para a direita ou vice-versa)
        direction = Vector3.right;
        if (Random.value > -0.5f)
        {
            direction = Vector3.left;
        }
    }

    void Update()
    {
        // Move o peixe na direção definida
        transform.Translate(direction * speed * Time.deltaTime);

        // Verifica se o peixe saiu da tela e reposiciona do outro lado
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            Vector3 newPos = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}