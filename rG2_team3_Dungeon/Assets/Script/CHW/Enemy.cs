using UnityEngine;
using UnityEngine.InputSystem.Processors;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // 움직임
    public float speed = 3;
    public float health;
    public Sprite[] sprites;
    Rigidbody2D target;

    // 생존 여부
    bool isLive = true;

    // 컴포넌트 변수
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        // 컴포넌트 변수 초기화
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        // 타겟 초기화
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
    }

    void FixedUpdate()
    {
        // 사망 시 정지
        if(!isLive)
        {
            return;
        }
        
        // 타겟을 향해 이동
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        // 시망 시 정지
        if(!isLive)
        {
            return;
        }
        
        // 좌우 반전
        spriter.flipX = target.position.x > rigid.position.x;
    }

    public void Init(SpawnData data)
    {
        // 스폰 데이터 반영
        spriter.sprite = sprites[data.spriteType];
        speed = data.speed;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        if(health > 0)
        {

        }
        else
        {
            Dead();
        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    IEnumerator KnockBack()
    {
        yield return new WaitForFixedUpdate();
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);
    }
}
