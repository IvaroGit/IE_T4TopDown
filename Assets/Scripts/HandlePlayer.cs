using UnityEngine;
using UnityEngine.InputSystem;

public class HandlePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    private Rigidbody2D _rb;

    private Vector2 _direction = Vector2.zero;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.linearVelocity = _direction * speed;
    }

    void OnMove(InputValue input)
    {
        _direction = input.Get<Vector2>();
    }
}
