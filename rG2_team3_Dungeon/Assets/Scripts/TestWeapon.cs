using System.Collections;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    public float speed;
    private bool isAttacking = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryAttack()
    {
        if (isAttacking) return;

        gameObject.SetActive(true);
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        float rotated = 0;
        isAttacking = true;

        yield return null;

        Transform Melee = GameManager.instance.pool.Get(0).transform;
        Melee.parent = this.transform;
        Melee.position = Vector2.zero;

        yield return null;

        Melee.Translate(Vector2.up.normalized * 1.5f, Space.Self);

        yield return null;

        while(rotated < 360)
        {
            float delta = speed * Time.deltaTime;
            transform.Rotate(Vector3.forward * delta);
            rotated += delta;

            yield return null;
        }

        ClearChildren();

        gameObject.SetActive(false);
        isAttacking = false;

        yield return new WaitForSeconds(0.3f);
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
