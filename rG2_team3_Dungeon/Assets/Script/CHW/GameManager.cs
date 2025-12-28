using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 각종 스크립트의 데이터 참조
    public static GameManager instance;

    public PoolManager pool;
    public Player player;

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
