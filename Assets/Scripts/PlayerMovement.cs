using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private readonly float _gravity = -0.981f;
    public float speed = 5f;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }
    
    private void FixedUpdate()
    {
        // rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        _rb.linearVelocity = new Vector2(_movement.x * speed, _movement.y + _gravity);
    }
}
