using UnityEngine;

public class Player : MonoBehaviour
{
    // 움직임
    Vector2 inputVec;
    public float speed = 5;
    
    // 컴포넌트 변수
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        // 컴포넌트 변수 초기화
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 방향 입력
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // 이동
        Vector2 nextVec = inputVec.normalized * Time.fixedDeltaTime * speed;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        // 좌우 반전
        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x > 0;
        }
    }


}
