using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static PoolManager instance;

    public GameObject[] prefabs;
    List<GameObject>[] pools;

    void Awake()
    {
        // --- 싱글톤 및 씬 전환 시 파괴 방지 로직 ---
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 넘어가도 오브젝트를 유지함
        }
        else
        {
            Destroy(gameObject); // 중복 생성된 인스턴스 파괴
            return;
        }

        // 풀 초기화
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 리스트를 순회
        foreach (GameObject item in pools[index])
        {
            // [수정] MissingReferenceException 방지: 오브젝트가 null이 아닐 때만 체크
            if (item != null)
            {
                if (!item.activeSelf)
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }
        }

        // 비활성화된 오브젝트가 없으면 새로 생성
        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}