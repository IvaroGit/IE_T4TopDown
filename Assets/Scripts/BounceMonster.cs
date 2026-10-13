public class BounceMonster : MonoBehaviour
{
    [SerializeField]
    private float force = 200;

    [SerializeField]
    private float newForceMin = 2;

    [SerializeField]
    private float newForceMax = 5;

    private Rigidbody2D _rb;

    private float _newForceTime = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _newForceTime -= Time.deltaTime;
        if (_newForceTime <= 0)
        {
            _newForceTime = Random.Range(newForceMin, newForceMax);

            _rb.AddForce(Random.onUnitCircle * force);
        }
    }
}
