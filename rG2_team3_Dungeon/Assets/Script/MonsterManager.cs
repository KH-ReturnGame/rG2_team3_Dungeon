using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    // 싱글톤 패턴을 위한 인스턴스
    public static MonsterManager Instance;

    public GameObject monsterPrefab; // 생성할 몬스터 프리팹
    public Transform[] spawnPoints; // 몬스터가 생성될 위치
    public List<Transform> monsterKind1_Nomal; // 이세계 (일반)
    public List<Transform> monsterKind1_Hard;  // 이세계 (하드)
    public List<Transform> monsterKind2_Nomal; // 사이버펑크 (일반)
    public List<Transform> monsterKind2_Hard;  // 사이버펑크 (하드)



    public float spawnInterval = 5f; // 몬스터 생성 주기

    private List<GameObject> activeMonsters = new List<GameObject>(); // 현재 활성화된 몬스터 리스트



    void Awake()
    {
        // 싱글톤 인스턴스 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않게 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 몬스터 생성
    }

    // 단일 몬스터 생성 메서드
    public void SpawnMonster()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("Spawn points not assigned!");
            return;
        }

        // 스폰 지점 중 하나를 무작위로 선택
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 몬스터 프리팹을 생성하고 리스트에 추가
        GameObject newMonster = Instantiate(monsterPrefab, randomSpawnPoint.position, Quaternion.identity);
        activeMonsters.Add(newMonster);
    }

    // 몬스터가 죽었을 때 호출될 메서드
    public void MonsterDied(GameObject monster)
    {
        // 리스트에서 몬스터 제거
        activeMonsters.Remove(monster);
        Destroy(monster);
    }
}