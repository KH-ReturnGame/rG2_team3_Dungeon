using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public float damagePerTick = 0.5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어 오브젝트에서 state_s 스크립트를 가져온다.
            state_s playerStats = other.GetComponent<state_s>();

            if (playerStats != null)
            {
                // 데미지를 입힌다.
                playerStats.TakeDamage(damagePerTick);
                Debug.Log("보스한테 맞음! 데미지: " + damagePerTick);
            }
        }
    }
}