using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���� ��ũ��Ʈ�� ������ ����
    public static GameManager instance;
    public Vector3 nextSpawnPoint;
    public PoolManager pool;
    public Player player;
    public state_s status;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
