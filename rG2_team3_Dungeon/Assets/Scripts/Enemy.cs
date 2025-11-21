using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public Rigidbody2D target;

    Rigidbody2D rb;
    SpriteRenderer render;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        render = rb.GetComponent<SpriteRenderer>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 dirVec = target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * Time.fixedDeltaTime * speed;
        rb.MovePosition(rb.position + nextVec);
        rb.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        render.flipX = target.position.x > rb.position.x;
    }
}
