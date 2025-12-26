using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch(id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    public void Init()
    {
        switch(id)
        {
            case 0:
                SetForm();
                break;
            default:
                break;
        }
    }

    void SetForm()
    {
            for (int i = 0; i < count; i++)
            {
                Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;

                Vector3 rotVec = Vector3.forward * 360 * i / count;
                bullet.Rotate(rotVec);
                bullet.Translate(Vector2.up * 1.2f, Space.Self);

                bullet.GetComponent<Bullet>().Init(damage, -1);
            }
    }
}
