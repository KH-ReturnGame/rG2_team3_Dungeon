using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public Vector3 nextSpawnPoint; // 다음 씬에서 나타날 위치 저장

    public float player_MaxHP = 100f;//최대체력
    public float player_Atack = 10f;//플레이어 공격력

    public bool skill_2 = false;
    public bool skill_3 = false;
    public bool skill_4 = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
}
