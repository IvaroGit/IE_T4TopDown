using UnityEngine;

public class Tear : MonoBehaviour
{
    [SerializeField] private float range = 10f;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(_startPosition, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }
}
