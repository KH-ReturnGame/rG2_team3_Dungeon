using UnityEngine;
using System.Collections.Generic; // List를 사용하기 위해 필요합니다.

// FloatRange 구조체 (이 코드를 MapMaker.cs 파일 밖, 예를 들어 Utilities.cs 등에 정의하거나, 이 파일 맨 위에 추가해야 합니다.)


public class NonOverlappingEdgeSpawner : MonoBehaviour
{
    
    public GameObject objectToSpawn;
    
    // ⭐ 스케일(크기)을 이 범위 내에서 무작위로 결정합니다.
    public FloatRange rangeOfScale = new FloatRange { min = 2f, max = 4f };

    // ⭐ 위치를 저장하는 리스트는 사용하지 않고, Physics.CheckBox를 사용합니다.
    // List<int> position = new List<int>(); // 주석 처리 또는 제거
    
    public int countToSpawn = 15;

    public float minX = -19f; 
    public float maxX = 22f;
    public float minY = -11f; 
    public float maxY = 11f;   

    public LayerMask spawnableLayer; 
    
    // Physics.CheckBox를 위한 박스 크기 (전체 크기)
    public Vector3 boxSize = new Vector3(2f, 2f, 2f); 
    public Quaternion boxRotation = Quaternion.identity; 

    // 이 변수는 필요 없습니다.
    // Vector3 spawnPosition; 


    // --- Unity 생명 주기 함수 ---
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SpawnObjectsWithCheck();
        }
    }

    // --- 주요 로직 함수 ---

    public void SpawnObjectsWithCheck()
    {
        int successfullySpawned = 0;
        // 최대 시도 횟수를 적절한 수준으로 낮춥니다.
        int maxAttempts = countToSpawn * 1000; 
        int attempt = 0;

        while (successfullySpawned < countToSpawn && attempt < maxAttempts)
        {
            // 1. 무작위 위치를 계산합니다.
            Vector3 randomPosition = GetRandomSpawnPosition();
            
            // 2. 해당 위치가 다른 오브젝트에 의해 점유되었는지 검사합니다.
            // ⭐ 수정: randomPosition을 인자로 전달해야 합니다.
            bool isOccupied = IsPositionOccupied(randomPosition); 
            
            if (!isOccupied) 
            {
                // 3. 비어있다면 오브젝트를 생성하고, 스케일에 무작위성을 적용합니다.
                GameObject newObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

                // ⭐ 스케일에 FloatRange를 적용합니다.
                float randomScale = rangeOfScale.GetRandom();
                newObject.transform.localScale = Vector3.one * randomScale;

                Debug.Log($"✅ 오브젝트 생성 완료 ({successfullySpawned + 1}/{countToSpawn}): {randomPosition} | 스케일: {randomScale:F2}");

                successfullySpawned++;
                
                // 리스트 사용 로직은 Physics 검사 방식으로 대체되었으므로 제거했습니다.
            }
            // else
            // {
            //     Debug.Log($"❌ 생성 실패 (위치 점유됨): {randomPosition}");
            // }

            attempt++;
        }

        if (successfullySpawned < countToSpawn)
        {
            Debug.LogWarning($"경고: 요청된 {countToSpawn}개 중 {successfullySpawned}개만 생성되었습니다. (총 시도 횟수: {attempt})");
        }
    }

    // 무작위 스폰 위치를 계산하여 Vector3를 반환하도록 함수 통합
    private Vector3 GetRandomSpawnPosition()
    {
        float targetX = Random.Range(minX, maxX);
        float targetY = Random.Range(minY, maxY);
        // 2D/평면 3D를 위해 Z는 0으로 설정합니다.
        return new Vector3(targetX, targetY, 0f);
    }
    
    // 주어진 위치에 다른 오브젝트가 있는지 물리 검사를 수행합니다.
    // ⭐ 수정: 검사할 위치를 매개변수로 받습니다.
    public bool IsPositionOccupied(Vector3 checkPosition) 
    {
        // Physics.CheckBox는 박스의 Half-Extents(절반 크기)를 받습니다.
        bool isOccupied = Physics.CheckBox(
            checkPosition, // ⭐ 매개변수로 받은 위치를 사용합니다.
            boxSize / 2f,  // Half-Extents
            boxRotation,
            spawnableLayer
        );
        
        return isOccupied;
    }

    // 디버깅을 위한 시각화
    private void OnDrawGizmos()
    {
        // 전체 스폰 영역 시각화
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0.1f);
        Gizmos.DrawWireCube(center, size);

        // 검사 박스 크기 시각화 (중앙에서)
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(center, boxRotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}