using UnityEngine;

public class Poobottle : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;

    [SerializeField]
    private float sightRange = 4;

    private Rigidbody2D _rb;
    private GameObject  _player;

    void Start()
    {
        _rb     = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 direction =
                _player.transform.position -
                transform.position;

        _rb.linearVelocity = Vector2.zero;

        if (direction.magnitude < sightRange)
        {
            /*
            Debug.DrawRay(
                    transform.position,
                    Quaternion.Euler(0, 0, Vector2.SignedAngle(direction, Vector2.right)) * Vector2.up,
                    Color.red,
                    1
            );
            */

            var hits =
                    Physics2D.CircleCast(
                            transform.position,
                            .5f,
                            direction,
                            new ContactFilter2D(),
                            sightRange);

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    continue;
                }

                if (hit
                    .collider
                    .gameObject
                    .CompareTag("Player"))
                {
                    break;
                }
                else
                {
                    return;
                }
            }

            _rb.linearVelocity =
                    direction.normalized
                    * speed;
        }
    }
}
