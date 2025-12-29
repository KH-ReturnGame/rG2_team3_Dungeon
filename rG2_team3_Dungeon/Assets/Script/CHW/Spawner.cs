using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 스폰 포인트 위치
    Transform[] spawnPoint;
    // 스폰 시 데이터
    public SpawnData[] spawnData;
    // 몬스터 종류
    int monsType;

    void Awake()
    {
       // 스폰 포인트 위치 초기화
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 몬스터 종류 초기화
        //monsType = Random.Range(0, GetComponent<Enemy>().sprites.Length);
    }

    public void Spawn()
    {
        // 몬스터 스폰
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        monsType = Random.Range(0, spawnData.Length);
        enemy.GetComponent<Enemy>().Init(spawnData[monsType]);
    }
}

[System.Serializable]
public class SpawnData
{
    // 스폰 시 데이터
    public int spriteType;
    public float health;
    public float speed;
    public float damage;
}
