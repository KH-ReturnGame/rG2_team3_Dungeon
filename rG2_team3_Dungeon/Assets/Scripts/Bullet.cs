using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int penetration;

    Rigidbody2D rigid;

    void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float dmg, int p, Vector3 d)
    {
        this.damage = dmg;
        this.penetration = p;
        
        if(penetration > -1)
        {
            rigid.linearVelocity = d;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Enemy") || collision.CompareTag("Tile"))) return;

        penetration--;

        if(penetration == -1)
        {
            gameObject.SetActive(false);
        }
    }
}
