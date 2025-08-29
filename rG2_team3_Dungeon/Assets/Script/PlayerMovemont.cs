using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // 이동 속도
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 키보드 입력 받기
        movement.x = Input.GetAxisRaw("Horizontal"); // A, D 또는 ←, →
        movement.y = Input.GetAxisRaw("Vertical");   // W, S 또는 ↑, ↓
    }

    void FixedUpdate()
    {
        // 물리 연산은 FixedUpdate에서 처리
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}