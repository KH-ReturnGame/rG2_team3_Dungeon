using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public Vector3 nextSpawnPoint;

    public float player_MaxHP = 100f;//�ִ�ü��
    public float player_Atack = 10f;//�÷��̾� ���ݷ�

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
