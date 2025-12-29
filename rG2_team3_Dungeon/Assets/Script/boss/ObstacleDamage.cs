using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public float damagePerTick = 0.5f; // 틱당 깎일 HP

    // 2D 콜라이더와 닿아 있는 동안 매 프레임 호출됨
    private void OnTriggerStay2D(Collider2D other)
    {
        // 충돌한 물체가 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            
        
            // 매 틱마다 데미지 전달
            Debug.Log("보스한테 맞은 틱!!!!");
            
        }
    }
}