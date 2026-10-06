using System.Collections;
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
    private float tearCooldown = .5f;

    [SerializeField]
    private int startingHearts = 3;

    [SerializeField]
    private GameObject heartDisplay;

    [SerializeField]
    private GameObject heart;

    [SerializeField]
    private GameObject halfHeart;

    private static readonly int SpeedXHash =
            Animator.StringToHash("Speed X");

    private static readonly int SpeedYHash =
            Animator.StringToHash("Speed Y");

    private Rigidbody2D    _rb;
    private SpriteRenderer _renderer;
    private Animator       _bodyAnimator;
    private Animator       _headAnimator;

    private Vector2 _direction = Vector2.zero;
    private Vector2 _tearDirection = Vector2.zero;

    private WaitForSeconds _tearCooldown;

    private int  hearts;
    private int  displayedHearts = 0;
    private bool _canCry         = true;

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

        _tearCooldown = new WaitForSeconds(tearCooldown);
    }

    void Update()
    {
        if (displayedHearts != hearts)
        {
            DisplayHearts();
        }

        if (_canCry && _tearDirection != Vector2.zero)
        {
            _headAnimator.SetFloat(SpeedXHash, _tearDirection.x);
            _headAnimator.SetFloat(SpeedYHash, _tearDirection.y);

            Shoot(_tearDirection);
        }

        _rb.linearVelocity = _direction * speed;
    }

    void OnMove(InputValue input)
    {
        _direction = input.Get<Vector2>();

        _bodyAnimator.SetFloat(SpeedXHash, _direction.x);
        _bodyAnimator.SetFloat(SpeedYHash, _direction.y);

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
        Vector2 finalDirection = Vector2.zero;

        if (direction.x < -.5f)
        {
            finalDirection.x = -1;
            finalDirection.y = 0;
        }

        if (direction.x > .5f)
        {
            finalDirection.x = 1;
            finalDirection.y = 0;
        }

        if (direction.y < -.5f)
        {
            finalDirection.x = 0;
            finalDirection.y = -1;
        }

        if (direction.y > .5f)
        {
            finalDirection.x = 0;
            finalDirection.y = 1;
        }

        _tearDirection = finalDirection;
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

        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        _canCry = false;

        yield return _tearCooldown;

        _canCry = true;
    }

    void DisplayHearts(bool addHalfHeart = false)
    {
        displayedHearts = hearts;

        foreach (Transform child in heartDisplay.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < hearts; i++)
        {
            var newHeart = Instantiate(heart);

            newHeart.transform.SetParent(heartDisplay.transform, false);
        }

        if (addHalfHeart)
        {
            var newHeart = Instantiate(halfHeart);

            newHeart.transform.SetParent(heartDisplay.transform, false);
        }
    }
}
