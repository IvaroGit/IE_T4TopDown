public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private Vector3 explosionOffset = new Vector3(0.1f, -0.25f, 0);

    [SerializeField]
    private float explosionDelay = 1;

    [SerializeField]
    private int numberOfFlashes = 10;

    [SerializeField]
    private float flashTime = 0.05f;

    [SerializeField]
    private Color flashColor = Color.red;

    SpriteRenderer _renderer;

    IEnumerator Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        yield return new WaitForSeconds(explosionDelay);

        Color color = _renderer.color;

        var wfs = new WaitForSeconds(flashTime);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            _renderer.color = flashColor;
            yield return wfs;
            _renderer.color = color;
            yield return wfs;
        }

        Instantiate(
                explosion,
                transform.position +
                explosionOffset,
                explosion.transform.rotation);

        Destroy(gameObject);
    }
}
