using UnityEngine;
using UnityEngine.InputSystem;

public class HandlePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    private static readonly int speedXHash =
            Animator.StringToHash("Speed X");

    private static readonly int speedYHash =
            Animator.StringToHash("Speed Y");

    private Rigidbody2D    _rb;
    private Animator       _bodyAnimator;
    private Animator       _headAnimator;
    private SpriteRenderer _renderer;

    private Vector2 _direction = Vector2.zero;

    void Start()
    {
        _rb           = GetComponent<Rigidbody2D>();
        _bodyAnimator = GetComponent<Animator>();
        _headAnimator = transform
                .Find("Head")
                .GetComponent<Animator>();
        _renderer = transform
                .Find("Body A")
                .GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _rb.linearVelocity = _direction * speed;
    }

    void OnMove(InputValue input)
    {
        _direction = input.Get<Vector2>();

        _bodyAnimator.SetFloat(speedXHash, _direction.x);
        _bodyAnimator.SetFloat(speedYHash, _direction.y);

        _headAnimator.SetFloat(speedXHash, _direction.x);
        _headAnimator.SetFloat(speedYHash, _direction.y);

        _renderer.flipX = _direction.x < 0;
    }
}
