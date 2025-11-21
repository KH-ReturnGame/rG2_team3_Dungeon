using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private Vector2 moveVec;

    Rigidbody2D rb;
    SpriteRenderer rend;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isLive) return;
        
        moveVec.x = Input.GetAxisRaw("Horizontal");
        moveVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive) return;

        Vector2 nextVec = moveVec.normalized * Time.fixedDeltaTime * speed;
        rb.MovePosition(rb.position + nextVec);
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive) return;

        if (moveVec.x != 0)
        {
            rend.flipX = moveVec.x < 0;
        }
    }
}
