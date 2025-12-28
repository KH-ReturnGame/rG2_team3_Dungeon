using UnityEngine;

public class PlayerState: MonoBehaviour
{
    public float P_max_HP = 0;//플레이어 최대 HP
    public float P_Atack = 0;// 플레이어 공격력

    public void Start()
    {
        P_max_HP = EventManager.instance.player_MaxHP;//플레이어 최대 HP
        P_Atack = EventManager.instance.player_Atack;// 플레이어 공격력

    }


}
