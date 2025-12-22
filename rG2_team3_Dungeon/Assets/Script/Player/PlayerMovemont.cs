using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // 이속
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        // 씬이 시작될 때 매니저에 저장된 위치가 있다면 그곳으로 이동
        /*if (GameManager.instance != null)
        {
            Debug.Log("tlqkf이동을 쳐 하라고");
            Vector3 targetPos = GameManager.instance.nextSpawnPoint;
            
            // 속도와 물리력을 초기화하여 순간이동 방해를 막음
            rb.linearVelocity = Vector2.zero;
            transform.position = targetPos;
            rb.position = targetPos;
            Debug.Log("이동 좌표: " + targetPos);
        }*///좆까 그냥 새로 만들꺼야 ㅅ;발
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}