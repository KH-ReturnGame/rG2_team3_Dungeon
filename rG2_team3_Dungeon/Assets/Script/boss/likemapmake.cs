using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class likemapmake : MonoBehaviour
{
    public Tilemap targetTilemap;
    public GameObject prefab;
    public int spawnCount = 20;
    public float minScale = 2f;
    public float maxScale = 8f;

    [Header("연출 설정")]
    public Color warningColor = new Color(1f, 0f, 0f, 0.5f);
    public float intervalTime = 1f;
    public float startDelay = 10f; // 시작 대기 시간 설정

    private List<Vector3> availableTilePositions = new List<Vector3>();
    private bool isRunning = false;

    void Start()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("타일맵이 할당되지 않았습니다!");
            return;
        }
        
        RefreshAvailableTiles();

        // 게임 시작과 동시에 대기 코루틴 실행
        StartCoroutine(StartAfterDelay());
    }

    // [추가] 지정된 시간만큼 기다린 후 루프를 실행하는 코루틴
    IEnumerator StartAfterDelay()
    {
        Debug.Log(startDelay + "초 뒤에 생성을 시작합니다.");
        yield return new WaitForSeconds(startDelay);
        
        isRunning = true;
        StartCoroutine(InfiniteSpawnRoutine());
    }

    void RefreshAvailableTiles()
    {
        availableTilePositions.Clear();
        BoundsInt bounds = targetTilemap.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (targetTilemap.HasTile(pos))
            {
                Vector3 worldPos = targetTilemap.GetCellCenterWorld(pos);
                availableTilePositions.Add(worldPos);
            }
        }
    }

    // Update문에서의 키 입력 체크는 제거하거나 유지할 수 있음 (현재는 자동 실행)
    void Update()
    {
        // 필요하다면 J키로 강제 시작 기능을 남겨둘 수 있음
        if (Input.GetKeyDown(KeyCode.J) && !isRunning)
        {
            //isRunning = true;
            //StopAllCoroutines(); // 대기 중일 수 있으므로 멈추고 즉시 시작
            //StartCoroutine(InfiniteSpawnRoutine());
        }
    }

    IEnumerator InfiniteSpawnRoutine()
    {
        Debug.Log("무한 생성 루프가 시작되었습니다.");
        while (true)
        {
            SpawnNewBatch();
            yield return new WaitForSeconds(intervalTime);
        }
    }

    void SpawnNewBatch()
    {
        if (availableTilePositions.Count == 0) return;

        List<GameObject> batch = new List<GameObject>();

        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, availableTilePositions.Count);
            Vector3 spawnPos = availableTilePositions[randomIndex];
            float randomScale = Random.Range(minScale, maxScale);

            GameObject newObj = Instantiate(prefab, spawnPos, Quaternion.identity);
            newObj.transform.localScale = Vector3.one * randomScale;

            if (newObj.TryGetComponent(out SpriteRenderer sr)) sr.color = warningColor;
            
            Collider2D col = newObj.GetComponent<Collider2D>() ?? newObj.AddComponent<CircleCollider2D>();
            col.enabled = false;

            batch.Add(newObj);
        }

        StartCoroutine(ObjectLifeCycle(batch));
    }

    IEnumerator ObjectLifeCycle(List<GameObject> batch)
    {
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject obj in batch)
        {
            if (obj != null)
            {
                if (obj.TryGetComponent(out SpriteRenderer sr)) sr.color = Color.white;
                if (obj.TryGetComponent(out Collider2D col)) col.enabled = true;
            }
        }

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject obj in batch)
        {
            if (obj != null) Destroy(obj);
        }
    }
}