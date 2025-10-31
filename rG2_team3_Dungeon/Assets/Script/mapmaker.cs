using UnityEngine;

public class NonOverlappingEdgeSpawner : MonoBehaviour
{
    
    public GameObject objectToSpawn;
    
    // 오브젝트의 개수
    public int countToSpawn = 10;

    public float minX = -19f; // X 범위 최소
    public float maxX = 22f;  // X 범위 최대
    public float minY = -11f; // Y 범위 최소
    public float maxY = 11f;  // Y 범위 최대

    // 겹침 방지 검사에 사용할 물리 설정
    public LayerMask spawnableLayer; // 충돌을 검사할 레이어 (필수 설정)
    
    // Physics.CheckBox를 위한 박스 크기 및 회전
    public Vector3 boxSize = new Vector3(2f, 2f, 2f); // 검사할 가상 박스의 전체 크기
    public Quaternion boxRotation = Quaternion.identity; // 박스의 회전값 (기본값)
    
    // --- Unity 생명 주기 함수 ---

    // Unity 시작 시 단 한 번 자동으로 호출됩니다.
    void Start()
    {
        
        //SpawnObjectsWithCheck();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SpawnObjectsWithCheck();
        }
        
    }

    // --- 주요 로직 함수 ---

    // 설정된 개수만큼 오브젝트 생성을 시도합니다.
    public void SpawnObjectsWithCheck()
    {
        int successfullySpawned = 0;
        int maxAttempts = 10000; // 무한 루프 방지를 위해 최대 시도 횟수를 제한합니다.
        int attempt = 0;

        while (successfullySpawned < countToSpawn && attempt < maxAttempts)
        {
            // 1. 무작위 위치를 계산합니다. (Z는 0으로 고정)
            Vector3 randomPosition = GetRandomSpawnPosition();
            
            // 2. 해당 위치가 다른 오브젝트에 의해 점유되었는지 검사합니다.
            bool isOccupied = IsPositionOccupied(randomPosition);
            
            if (!isOccupied) 
            {
                // 3. 비어있다면 오브젝트를 생성합니다.
                Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
                Debug.Log($"✅ 오브젝트 생성 완료 ({successfullySpawned + 1}/{countToSpawn}): {randomPosition}");
                successfullySpawned++;
            }
            else
            {
                // 4. 점유되었다면 다음 시도로 넘어갑니다.
                Debug.Log($"❌ 생성 실패 (위치 점유됨): {randomPosition}");
            }
            
            attempt++;
        }

        if (successfullySpawned < countToSpawn)
        {
            Debug.LogWarning($"경고: 요청된 {countToSpawn}개 중 {successfullySpawned}개만 생성되었습니다. 나머지 위치를 찾지 못했습니다.");
        }
    }

    // 무작위 스폰 위치를 계산하여 반환합니다.
    private Vector3 GetRandomSpawnPosition()
    {
        float targetX = Random.Range(minX, maxX);
        float targetY = Random.Range(minY, maxY);
        // 2D/평면 3D를 위해 Z는 0으로 설정합니다.
        return new Vector3(targetX, targetY, 0f);
    }
    
    // 주어진 위치에 다른 오브젝트가 있는지 물리 검사를 수행합니다.
    public bool IsPositionOccupied(Vector3 spawnPosition)
    {
        // Physics.CheckBox는 충돌 여부(True/False)만 반환합니다.
        // boxSize / 2: Half-Extents(크기의 절반)를 인자로 전달합니다.
        bool isOccupied = Physics.CheckBox(
            spawnPosition,
            boxSize,
            boxRotation,
            spawnableLayer
        );
        
        return isOccupied;
    }

}
