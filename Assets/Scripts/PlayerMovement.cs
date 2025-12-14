using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private static readonly int MoveX = Animator.StringToHash("X");
    private static readonly int MoveY = Animator.StringToHash("Y");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    public float speed = 5f;
    private Animator _animator;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        // _rb.MovePosition(_rb.position + _movement * Time.fixedDeltaTime);
        // _rb.linearVelocity = new Vector2(_movement.x * speed, _movement.y * speed);
        _rb.linearVelocity = _movement * speed;
        if (_movement is { x: 0, y: 0 }) {
            _animator.SetBool(IsWalking, false);
        } else {
            _animator.SetFloat(MoveX, _movement.x);
            _animator.SetFloat(MoveY, _movement.y);
            _animator.SetBool(IsWalking, true);
        }
    }

    private void OnMovement(InputValue value) {
        _movement = value.Get<Vector2>();
    }
}