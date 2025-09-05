using UnityEngine;

public class NonOverlappingEdgeSpawner : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public Transform object3;
    public Transform object4;

    public GameObject objectToSpawn;
    public int numberOfObjectsPerSide = 5;

    // 무작위로 흩어질 최대 반경
    public float maxRandomOffset = 0.5f;

    // 겹침 방지 설정
    public float spawnRadius = 0.5f; // 생성할 오브젝트의 반경
    public float safeDistance = 1.0f; // 경계 오브젝트로부터 안전하게 떨어져 있을 거리
    public int maxAttemptsPerObject = 10; // 겹치지 않는 위치를 찾기 위한 최대 시도 횟수

    void Start()
    {
        SpawnObjectsAlongEdge(object1, object2, numberOfObjectsPerSide); // 위
        SpawnObjectsAlongEdge(object2, object3, numberOfObjectsPerSide); // 오른쪽
        SpawnObjectsAlongEdge(object3, object4, numberOfObjectsPerSide); // 아래
        SpawnObjectsAlongEdge(object4, object1, numberOfObjectsPerSide); // 왼쪽
    }

    void SpawnObjectsAlongEdge(Transform start, Transform end, int count)
    {
        int spawnedCount = 0;
        int attempts = 0;

        // while 루프를 사용하여 원하는 개수만큼 성공적으로 생성될 때까지 반복
        while (spawnedCount < count && attempts < maxAttemptsPerObject * count)
        {
            // 1. start와 end 사이의 무작위 위치를 선형 보간으로 계산
            float t = Random.Range(0.0f, 1.0f);
            Vector3 basePosition = Vector3.Lerp(start.position, end.position, t);

            // 2. 무작위 오프셋을 더해 최종 위치 결정
            Vector3 randomOffset = new Vector3(
                Random.Range(-maxRandomOffset, maxRandomOffset),
                Random.Range(-maxRandomOffset, maxRandomOffset),
                Random.Range(-maxRandomOffset, maxRandomOffset)
            );
            Vector3 finalPosition = basePosition + randomOffset;

            // 3. 경계 오브젝트와 너무 가깝지 않은지 확인
            if (Vector3.Distance(finalPosition, start.position) < safeDistance ||
                Vector3.Distance(finalPosition, end.position) < safeDistance)
            {
                attempts++;
                continue; // 다음 위치 시도
            }
            
            // 4. Physics.CheckSphere를 사용해 다른 오브젝트와의 겹침 여부를 확인
            bool isOverlapping = Physics.CheckSphere(finalPosition, spawnRadius);

            // 5. 겹치지 않으면 오브젝트를 생성하고 카운트 증가
            if (!isOverlapping)
            {
                Instantiate(objectToSpawn, finalPosition, Quaternion.identity);
                spawnedCount++;
            }
            
            attempts++;
        }

        if (attempts >= maxAttemptsPerObject * count)
        {
            Debug.LogWarning("겹치지 않는 오브젝트 " + count + "개를 모두 생성하는 데 실패했습니다. 범위를 넓히거나 spawnRadius를 줄여보세요.");
        }
    }
}