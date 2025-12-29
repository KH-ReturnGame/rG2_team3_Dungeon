using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BossStatus : MonoBehaviour
{
    public float maxHealth = 800f;
    public float health;
    public Slider bossHealthSlider;
    public TextMeshProUGUI bossHealthText;

    void Start() {
        health = maxHealth;
        UpdateBossUI();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null) {
                health -= bullet.damage;
                UpdateBossUI();
                Destroy(other.gameObject); // 총알 제거
                if (health <= 0) SceneManager.LoadScene("ClearScene");
            }
        }
    }

    void UpdateBossUI() {
        if (bossHealthSlider != null) bossHealthSlider.value = health / maxHealth;
        if (bossHealthText != null) bossHealthText.text = $"BOSS: {Mathf.Max(0, (int)health)}";
    }
}