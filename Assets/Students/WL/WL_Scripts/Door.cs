using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private float timer = 20f;

    [SerializeField]
    private float slideSpeed = 2f;

    [SerializeField]
    private float slideDistance = 3f;

    private Vector3 targetPos;

    private bool isOpen = false;
    private SpriteRenderer _renderer;
    private Collider2D _col;
    
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
    }

    void Update()
    {
       if (!isOpen)
       {
        timer -= Time.deltaTime;
        if (timer <= 0f) Open();
        return;
       }
    }

    void Open()
    {
        isOpen = true;
        targetPos = transform.position + Vector3.up * slideDistance;
        _col.enabled = false;
    }
}
