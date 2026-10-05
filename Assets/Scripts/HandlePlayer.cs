using UnityEngine;
using UnityEngine.InputSystem;

public class HandlePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private GameObject bomb;

    [SerializeField]
    private GameObject tear;

    [SerializeField]
    private float tearSpeed = 5;

    [SerializeField]
    private Transform tearPosition;

    [SerializeField]
    private int startingHearts = 3;

    [SerializeField]
    private GameObject heartDisplay;

    [SerializeField]
    private GameObject heart;

    private static readonly int SpeedXHash =
            Animator.StringToHash("Speed X");

    private static readonly int SpeedYHash =
            Animator.StringToHash("Speed Y");

    private Rigidbody2D    _rb;
    private SpriteRenderer _renderer;
    private Animator       _bodyAnimator;
    private Animator       _headAnimator;

    private Vector2 _direction = Vector2.zero;

    private int hearts;

    void Start()
    {
        hearts = startingHearts;

        _rb           = GetComponent<Rigidbody2D>();
        _bodyAnimator = GetComponent<Animator>();
        _headAnimator = transform
                .Find("Head")
                .GetComponent<Animator>();
        _renderer = transform
                .Find("Body")
                .GetComponent<SpriteRenderer>();

        DisplayHearts();
    }

    void Update()
    {
        _rb.linearVelocity = _direction * speed;
    }

    void OnMove(InputValue input)
    {
        _direction = input.Get<Vector2>();

        _bodyAnimator.SetFloat(SpeedXHash, _direction.x);
        _bodyAnimator.SetFloat(SpeedYHash, _direction.y);

        _headAnimator.SetFloat(SpeedXHash, _direction.x);
        _headAnimator.SetFloat(SpeedYHash, _direction.y);

        _renderer.flipX = _direction.x < 0;
    }

    void OnBomb(InputValue v)
    {
        Instantiate(
                bomb,
                transform.Find("Bomb Position").position,
                bomb.transform.rotation);
    }

    void OnShoot(InputValue v)
    {
        Vector2 direction = v.Get<Vector2>();

        if (direction.x > .5f)
        {
            Shoot(Vector2.right);

            return;
        }

        if (direction.x < -.5f)
        {
            Shoot(Vector2.left);

            return;
        }

        if (direction.y > .5f)
        {
            Shoot(Vector2.up);

            return;
        }

        if (direction.y < -.5f)
        {
            Shoot(Vector2.down);

            return;
        }
    }

    private void Shoot(Vector2 direction)
    {
        GameObject newTear = Instantiate(
                tear,
                tearPosition.position,
                tear.transform.rotation
        );

        newTear.GetComponent<Rigidbody2D>().linearVelocity =
                direction * tearSpeed;
    }

    void DisplayHearts()
    {
        /*
        while (heartDisplay.transform.childCount > 0)
        {
            Destroy(heartDisplay.transform.GetChild(0).gameObject);
        }
*/
        for (int i = 0; i < hearts; i++)
        {
            var newHeart = Instantiate(heart);

            newHeart.transform.SetParent(heartDisplay.transform, false);
        }
    }
}
