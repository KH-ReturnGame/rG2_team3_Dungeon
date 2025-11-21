using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isLive;

    public float health;
    public float maxHealth;
    public float speed;

    public PlayerMove player;
    
    void Awake()
    {
        Instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        player.speed = speed;

        isLive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
