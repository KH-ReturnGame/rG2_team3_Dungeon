using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리에 필요

public class state_s : MonoBehaviour
{
    public int Shop_Hp = 0;      
    public int Shop_Atack = 0;   
    public int Shop_speed = 0;


    public float player_Atack = 10f;
    public float player_MaxHP = 100f;
    public float player_Hp;
    public float moveSpeed = 10f;

    public bool ad = false;

    [Header("돌진 (Skill 1)")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 80f; 
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.right; 

    [Header("무적 (Skill 2)")]
    public float invincibleDuration = 1f;
    public float cooldownTime = 6f;
    public Color invincibleColor = Color.yellow;
    private bool isInvincible = false;
    private bool isskill_2Cooldown = false;
    private Color originalColor;  

    void Awake()
    {   
        player_Hp = player_MaxHP;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 탑뷰 설정
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }
    void Start()
    {
        
    }
    public void Update()
    {
        if (isDashing) return;

        // 1. 입력 감지
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 2. 마지막 방향 저장 (돌진용)
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput.normalized;
            
            // 좌우 반전
            if (moveInput.x != 0)
            {
                spriteRenderer.flipX = (moveInput.x < 0);
            }
        }

        // [E] 무적 스킬
        if (Input.GetKeyDown(KeyCode.E) && !isskill_2Cooldown && Shop_speed >= 5)
        {
            StartCoroutine(skill_2());
        }

        // [Q] 돌진 스킬
        if (Input.GetKeyDown(KeyCode.Q) && canDash && Shop_speed >= 2)
        {
            StartCoroutine(Dash());
        }
    }

    public void FixedUpdate()
    {
        if (isDashing) return;

        // 이동 처리 (PlayerMovement의 로직을 이쪽으로 통합)
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // 돌진 물리 적용
        rb.linearVelocity = lastMoveDirection * dashingPower;

        yield return new WaitForSeconds(dashingTime);

        isDashing = false;
        rb.linearVelocity = Vector2.zero; // 돌진 후 정지

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator skill_2()
    {
        isInvincible = true;
        isskill_2Cooldown = true;
        if (spriteRenderer != null) spriteRenderer.color = invincibleColor;
        Debug.Log("무적시작");
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
        Debug.Log("무적끝");
        if (spriteRenderer != null) spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(cooldownTime - invincibleDuration);
        isskill_2Cooldown = false;
        Debug.Log("쿨 돌았음");
    }

    void OnEnable()
    {
        // 씬 로드 이벤트 연결
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 이벤트 연결 해제 (메모리 누수 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때마다 실행됨
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name + " 씬이 로드되었습니다!");
        /*player_Atack = player_Atack + 4f*Shop_Atack;
        player_MaxHP = player_MaxHP + 10f*Shop_Hp;
        moveSpeed = moveSpeed + Shop_speed; */
        Debug.Log("스텟 초기화");
        player_Atack = player_Atack + 4f*Shop_Atack;
        player_MaxHP = player_MaxHP + 10f*Shop_Hp;
        moveSpeed = moveSpeed + Shop_speed; 
        if(Shop_speed >= 10)
        {
            player_Atack = (player_Atack + 4f*Shop_Atack)*moveSpeed/20;

        }
        if(ad == true)
        {
            if(player_MaxHP/player_Hp>10)
            {
                player_Atack = player_Atack*11;
            }
            else
            {
                player_Atack = player_Atack + player_MaxHP/player_Hp;
            }
        }
    }
    
}