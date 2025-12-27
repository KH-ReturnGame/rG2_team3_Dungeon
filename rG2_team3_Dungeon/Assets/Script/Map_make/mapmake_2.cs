using UnityEngine;
using UnityEngine.Tilemaps; // 타일맵 사용을 위해 필수

public class mapmake_2 : MonoBehaviour
{
    public Tilemap targetTilemap;  // 범위를 가져올 타일맵
    public GameObject prefab;
    public int spawnCount = 20;
    public float minScale = 0.5f;
    public float maxScale = 2.0f;
    public int maxAttempts = 50;

    void Start()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("타일맵이 할당되지 않았습니다!");
            return;
        }
        
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SpawnObjectsOnTilemap();
        }
    }

    void SpawnObjectsOnTilemap()
    {
        // 타일맵의 실제 타일들이 차지하는 영역(Bounds)을 가져옴
        Bounds bounds = targetTilemap.localBounds;

        for (int i = 0; i < spawnCount; i++)
        {
            bool placed = false;
            int attempts = 0;

            while (!placed && attempts < maxAttempts)
            {
                attempts++;

                // 1. 타일맵 범위 내에서 무작위 좌표 계산
                float randomX = Random.Range(bounds.min.x, bounds.max.x);
                float randomY = Random.Range(bounds.min.y, bounds.max.y);
                Vector3 spawnPos = new Vector3(randomX, randomY, 0);

                // 월드 좌표로 변환 (타일맵의 위치/회전/스케일 반영)
                Vector3 worldPos = targetTilemap.transform.TransformPoint(spawnPos);

                float randomScale = Random.Range(minScale, maxScale);
                float radius = randomScale / 2f;

                // 2. 겹침 검사
                Collider2D hit = Physics2D.OverlapCircle(worldPos, radius);

                if (hit == null)
                {
                    // 3. 배치
                    GameObject newObj = Instantiate(prefab, worldPos, Quaternion.identity);
                    newObj.transform.localScale = Vector3.one * randomScale;

                    // Collider가 없는 경우를 대비해 동적 추가
                    if (newObj.GetComponent<Collider2D>() == null)
                    {
                        newObj.AddComponent<CircleCollider2D>();
                    }

                    placed = true;
                }
            }
        }
    }
}