using System.Collections;
using UnityEngine;

public class HomingSkull : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float maxAngleChange = 1;

    [SerializeField]
    private float maxAngleDifference = 10;

    [SerializeField]
    private bool delayOnDifference = true;

    [SerializeField]
    private GameObject tear;

    [SerializeField]
    private float tearRange = 3;

    [SerializeField]
    private float tearAngleSpan = 5;

    [SerializeField]
    private float coolDownMax = 2;

    [SerializeField]
    private float tearSpeed = 5;

    private Rigidbody2D _rb;
    private GameObject  _player;

    private WaitForSeconds _wfs;

    private bool _canFire = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _player = GameObject.FindWithTag("Player");

        _wfs = new WaitForSeconds(coolDownMax);
    }

    void Update()
    {
        Vector2 direction =
                _player.transform.position -
                transform.position;

        float targetAngle =
                Vector2.SignedAngle(direction, Vector2.up);

        float currentAngle = transform.eulerAngles.z;

        float newAngle =
                Mathf.LerpAngle(
                        currentAngle,
                        180 - targetAngle,
                        maxAngleChange * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, 0, newAngle);

        bool inAngleSpan =
                Mathf.Abs(currentAngle - newAngle) <
                maxAngleDifference * Time.deltaTime;

        if (delayOnDifference && !inAngleSpan)
        {
            return;
        }

        inAngleSpan =
                Mathf.Abs(currentAngle - newAngle) <
                tearAngleSpan * Time.deltaTime;

        if (_canFire && inAngleSpan && direction.sqrMagnitude > tearRange)
        {
            var newTear = Instantiate(
                    tear,
                    transform.position,
                    Quaternion.identity);

            newTear
                    .GetComponent<Rigidbody2D>()
                    .linearVelocity = direction.normalized * tearSpeed;

            StartCoroutine(CoolDownRoutine());
        }

        _rb.linearVelocity = -transform.up * speed;
    }

    IEnumerator CoolDownRoutine()
    {
        _canFire = false;

        yield return _wfs;

        _canFire = true;
    }
}
