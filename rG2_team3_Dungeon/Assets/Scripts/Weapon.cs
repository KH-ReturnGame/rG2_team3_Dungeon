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

    bool isAttacking;

    public void Attack(Vector3 targetPos)
    {
        if (isAttacking) return;

        switch (id)
        {
            case 0:
                StartCoroutine(MeleeAttack());
                break;
            case 1:
                RangeAttack(targetPos);
                break;
        }
    }

    IEnumerator MeleeAttack()
    {
        isAttacking = true;

        GameObject meleeObj = new GameObject("MeleeAttack");
        meleeObj.transform.position = transform.position;
        meleeObj.transform.parent = transform;

        SetForm(meleeObj);

        float rotated = 0f;

        while (rotated < 360f)
        {
            float delta = speed * Time.deltaTime;
            meleeObj.transform.Rotate(Vector3.forward * delta);
            rotated += delta;
            yield return null;
        }

        Destroy(meleeObj);

        isAttacking = false;
    }

    void RangeAttack(Vector3 targetPos)
    {
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;

        Vector3 dir = (targetPos - transform.position).normalized;
        bullet.GetComponent<Bullet>().Init(damage, 0, dir * speed);
    }

    void SetForm(GameObject Obj)
    {
            for (int i = 0; i < count; i++)
            {
                Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = Obj.transform;
                bullet.position = Vector2.zero;

                Vector3 rotVec = Vector3.forward * 360 * i / count;
                bullet.Rotate(rotVec);
                bullet.Translate(Vector2.up * 1.2f, Space.Self);

                bullet.GetComponent<Bullet>().Init(damage, -1, Vector2.zero);
            }
    }
}
