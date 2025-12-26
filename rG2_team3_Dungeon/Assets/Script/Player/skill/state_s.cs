using UnityEngine;
using System.Collections;

public class state_s : MonoBehaviour
{
    public int Shop_Hp = 0;      
    public int Shop_Atack = 0;   
    public int Shop_speed = 0;   

    [Header("Dash (2D Skill 1)")]
    private Rigidbody2D rb;           // 2D용 리지드바디
    public float dashForce = 15f;    // 2D에서는 3D보다 작은 값이 적당할 수 있음
    public float dashDuration = 0.2f; 
    public float dashCooldown = 4f;   
    private bool isskill_1Cooldown = false; 

    [Header("Invincible (Skill 2)")]
    public float invincibleDuration = 1f;
    public float cooldownTime = 6f;
    public Color invincibleColor = Color.yellow; 
    private bool isInvincible = false;
    private bool isskill_2Cooldown = false;
    private SpriteRenderer spriteRenderer; // 2D는 보통 Renderer 대신 SpriteRenderer 사용
    private Color originalColor;  

    void Awake()
    {
        // 2D 컴포넌트 연결
        rb = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void Update()
    {
        // [E] 무적 스킬
        if (Input.GetKeyDown(KeyCode.E) && !isskill_2Cooldown && Shop_speed >= 5)
        {
            StartCoroutine(skill_2());
        }

        // [Q] 돌진 스킬
        if (Input.GetKeyDown(KeyCode.Q) && !isskill_1Cooldown && Shop_speed >= 2)
        {
            //StartCoroutine(skill_1());
        }
    }

    /*IEnumerator skill_1() // 2D 돌진
    {
        isskill_1Cooldown = true;
        Debug.Log("2D 돌진 사용!");
        //돌진 만들기
        isskill_1Cooldown = false;
        Debug.Log("돌진 쿨타임 종료");
    }*/

    IEnumerator skill_2() // 무적
    {
        Debug.Log("무적 시작");
        isInvincible = true;
        isskill_2Cooldown = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = invincibleColor;
        }

        yield return new WaitForSeconds(invincibleDuration);
        
        isInvincible = false;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
        Debug.Log("무적 종료");

        yield return new WaitForSeconds(cooldownTime - invincibleDuration);

        isskill_2Cooldown = false;
        Debug.Log("무적 쿨타임 종료");
    }
}