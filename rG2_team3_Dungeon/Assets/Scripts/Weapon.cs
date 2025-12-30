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

    bool isAttacking = false;

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
        float rotated = 0;
        isAttacking = true;
        yield return null;

        SetForm();
        yield return null;

        while (rotated < 360)
        {
            float delta = speed * Time.deltaTime;
            transform.Rotate(Vector3.forward * delta);
            rotated += Mathf.Abs(delta);
            yield return null;
        }

        ClearChildren();
        gameObject.SetActive(false);
        isAttacking = false;
    }

    void RangeAttack(Vector3 targetPos)
    {
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;

        Vector3 dir = (targetPos - transform.position).normalized;
        bullet.GetComponent<Bullet>().Init(damage, 0, dir * speed);
    }

    void SetForm()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;
            bullet.position = transform.position;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(Vector2.up * 1.2f, Space.Self);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector2.zero);
        }
    }

    void ClearChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            child.SetParent(null);
            child.gameObject.SetActive(false);
        }
    }
}
