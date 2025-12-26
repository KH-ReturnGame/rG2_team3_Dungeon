using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int penetration;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(float dmg, int p)
    {
        this.damage = dmg;
        this.penetration = p;
    }
}
