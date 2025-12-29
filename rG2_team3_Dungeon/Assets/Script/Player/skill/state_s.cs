using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TMP 사용을 위해 필수

public class state_s : MonoBehaviour
{
    [Header("Shop & Player Stats")]
    public int Shop_Hp = 0; public int Shop_Atack = 0; public int Shop_speed = 0;
    public float player_Atack = 10f;
    public float player_MaxHP = 100f;
    public float player_Hp;
    public float moveSpeed = 10f;
    public bool ad = false;

    [Header("Dash (Skill 1)")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 20f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    [Header("Invincible (Skill 2)")]
    public float invincibleDuration = 1f;
    public float cooldownTime = 6f;
    public Color invincibleColor = Color.yellow;
    private bool isInvincible = false;
    private bool isskill_2Cooldown = false;
    private Color originalColor;

    [Header("UI & Components")]
    public TextMeshProUGUI hpText; 
    public GameObject gameOverUI; 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.right;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (rb != null) {
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (spriteRenderer != null) originalColor = spriteRenderer.color;
        player_Hp = player_MaxHP;
    }

    void Update() {
        if (isDashing || Time.timeScale == 0) return;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput != Vector2.zero) {
            lastMoveDirection = moveInput.normalized;
            if (moveInput.x != 0) spriteRenderer.flipX = (moveInput.x > 0); 
        }

        if (Input.GetKeyDown(KeyCode.E) && !isskill_2Cooldown && Shop_speed >= 5) StartCoroutine(skill_2());
        if (Input.GetKeyDown(KeyCode.Q) && canDash && Shop_speed >= 2) StartCoroutine(Dash());
        
        UpdateHPUI();
    }

    void FixedUpdate() {
        if (isDashing) return;
        // 가장 안정적인 물리 이동 방식
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage) {
        if (isInvincible) return;
        player_Hp -= damage;
        if (player_Hp <= 0) {Die();}
    }

    void UpdateHPUI() {
        if (hpText != null) hpText.text = $"HP: {Mathf.CeilToInt(player_Hp)} / {player_MaxHP}";
    }

    void Die() {
        Debug.Log("Tlqkf");
        
        if (gameOverUI != null) gameOverUI.SetActive(true);
        SceneManager.LoadScene("gameover"); 
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Dash() {
        canDash = false; isDashing = true;
        rb.linearVelocity = lastMoveDirection * dashingPower;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false; rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator skill_2() {
        isInvincible = true; isskill_2Cooldown = true;
        spriteRenderer.color = invincibleColor;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false; spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(cooldownTime - invincibleDuration);
        isskill_2Cooldown = false;
    }

    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { UpdateHPUI(); }
}