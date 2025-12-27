using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
                StopCoroutine(MeleeAttack());
                StartCoroutine(MeleeAttack());
                break;
            case 1:
                RangeAttack(targetPos);
                break;
        }
    }

    IEnumerator MeleeAttack()
    {
        float rotated = 0;

        GameObject meleeObj = new GameObject("MeleeAttack");
        meleeObj.transform.position = transform.position;
        meleeObj.transform.parent = transform;
        yield return null;

        SetForm(meleeObj);
        yield return null;

        float delta = speed * Time.deltaTime;
        transform.Rotate(Vector3.forward * delta);
        rotated += Mathf.Abs(delta);

        if(rotated >= 360)
        {
            yield return null;
        }

        Destroy(meleeObj);
    }

    void RangeAttack(Vector3 targetPos)
    {
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;

        Vector3 dir = (targetPos - transform.position).normalized;
        bullet.GetComponent<Bullet>().Init(damage, 0, dir * speed);
    }

    void SetForm(GameObject obj)
    {
            for (int i = 0; i < count; i++)
            {
                Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = obj.transform;
                bullet.position = Vector2.zero;

                Vector3 rotVec = Vector3.forward * 360 * i / count;
                bullet.Rotate(rotVec);
                bullet.Translate(Vector2.up * 1.2f, Space.Self);

                bullet.GetComponent<Bullet>().Init(damage, -1, Vector2.zero);
            }
    }
}
