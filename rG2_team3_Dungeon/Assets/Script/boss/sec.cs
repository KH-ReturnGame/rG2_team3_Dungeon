using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sec : MonoBehaviour
{
    public float appearanceDelay = 2.2f; // 2.2초 대기
    public float moveSpeed = 6f;         // 1초에 6 씩 이동
    public float targetDistance = 140f;  // 총 이동 거리

    private List<GameObject> childModels = new List<GameObject>();
    private Vector3[] startPositions;    // 자식들의 초기 위치 저장용

    void Awake()
    {

        int childCount = transform.childCount;
        startPositions = new Vector3[childCount];

        int i = 0;
        foreach (Transform child in transform)
        {
            childModels.Add(child.gameObject);
            startPositions[i] = child.localPosition; // 초기 로컬 위치 기록
            child.gameObject.SetActive(false);
            i++;
        }
        ReceiveSignal();
    }

    public void ReceiveSignal()
    {
        StopAllCoroutines();
        // 초기화: 다시 시작할 때 위치를 원래대로 돌려놓음
        ResetPositions();
        StartCoroutine(ObstacleSequence());
    }

    IEnumerator ObstacleSequence()
    {
        // 1. 2.2초 대기
        yield return new WaitForSeconds(appearanceDelay);

        // 2. 모든 자식 활성화
        SetAllChildren(true);

        // 3. 140만큼 이동할 때까지 매 프레임 이동
        float movedDistance = 0f;
        while (movedDistance < targetDistance)
        {
            float moveStep = moveSpeed * Time.deltaTime;
            
            foreach (GameObject child in childModels)
            {
                if (child != null)
                {
                    // 위쪽 방향(transform.up)으로 이동 (방향은 상황에 따라 Vector3.up 등으로 변경 가능)
                    child.transform.Translate(Vector3.right  * moveStep);
                }
            }

            movedDistance += moveStep;
            yield return null; // 다음 프레임까지 대기
        }

        // 4. 이동 완료 후 비활성화
        SetAllChildren(false);
    }

    void SetAllChildren(bool state)
    {
        foreach (GameObject child in childModels)
        {
            if (child != null) child.SetActive(state);
        }
    }

    void ResetPositions()
    {
        for (int i = 0; i < childModels.Count; i++)
        {
            if (childModels[i] != null)
                childModels[i].transform.localPosition = startPositions[i];
        }
    }
}