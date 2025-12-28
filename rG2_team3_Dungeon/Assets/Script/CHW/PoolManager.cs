using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    // 프리팹 저장
    public GameObject[] prefabs;

    // 오브젝트 풀
    List<GameObject>[] pools;

    void Awake()
    {
        // 풀 초기화
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        // Enemy 선택
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if(select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
