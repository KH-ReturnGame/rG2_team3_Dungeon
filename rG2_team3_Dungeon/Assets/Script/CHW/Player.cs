using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    // 움직임
    Vector2 inputVec;
    public float speed = 5;
    public float health = 160;
    
    // 컴포넌트 변수
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    public Weapon meleeWeapon;
    public Weapon rangeWeapon;

    public float attackRange = 2.5f;
    public float attackCooldown = 0.5f;

    float lastAttackTime;
    private bool isTakingDamage = false;

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

        if (Input.GetMouseButtonDown(1))
        {
            TryAttack();
        }
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

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        Vector3 mouseWorldPos = GetMouseWorldPosition();
        float distance = Vector3.Distance(transform.position, mouseWorldPos);

        if (distance <= attackRange)
        {
            meleeWeapon.gameObject.SetActive(true);
            meleeWeapon.Attack(mouseWorldPos);
        }
        else
        {
            rangeWeapon.Attack(mouseWorldPos);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!isTakingDamage)
                StartCoroutine(DamageCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isTakingDamage = false;
        }
    }

    IEnumerator DamageCoroutine()
    {
        isTakingDamage = true;

        while (isTakingDamage)
        {
            TakeDamage(GetComponent<Enemy>().damage);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log($"플레이어 데미지: {dmg}");
    }
}
