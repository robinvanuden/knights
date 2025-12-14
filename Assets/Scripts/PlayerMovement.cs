using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {
    private static readonly int MoveX = Animator.StringToHash("X");
    private static readonly int MoveY = Animator.StringToHash("Y");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    public float speed = 5f;
    private Animator _animator;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    
    public bool freeMovement = false;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
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
        // TODO: Decide to remove or add free movement
        if (freeMovement){
            _movement = value.Get<Vector2>();
        } else {
            // 1-directional movement (no diagonals): prefer the axis with the larger magnitude.
            var movement = value.Get<Vector2>();
            // If both inputs are near zero, stop completely
            if (Mathf.Approximately(movement.x, 0f) && Mathf.Approximately(movement.y, 0f)) {
                _movement = Vector2.zero;
                return;
            }

            // Choose dominant axis to avoid diagonal movement
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y)) {
                _movement = new Vector2(Mathf.Sign(movement.x), 0f);
            } else {
                _movement = new Vector2(0f, Mathf.Sign(movement.y));
            }
        }
    }

    private void OnInteract(InputValue value) {
        Debug.Log("Interact!");
        var fog = GameObject.Find("Fog")?.GetComponent<Tilemap>();
        if (fog) {
            fog.GetComponent<FogController>()?.ClearFog();
        }
        
    }
}