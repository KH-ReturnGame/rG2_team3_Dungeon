using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int penetration;

    Vector3 dir;

    public void Init(float dmg, int p, Vector3 d)
    {
        this.damage = dmg;
        this.penetration = p;
        this.dir = d;
    }

    void Update()
    {
        transform.position += (Vector3)(dir * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || !collision.CompareTag("Tile"))
            return;

        if (penetration > 0)
        {
            penetration--;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
