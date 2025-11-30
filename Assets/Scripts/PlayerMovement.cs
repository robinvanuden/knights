using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    
    private void FixedUpdate()
    {
        // rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(movement.x * speed, movement.y);
    }
}
