using UnityEngine;
using System.Collections;

public class BossChildController : MonoBehaviour
{
    [Header("참조 설정")]
    public GameObject childToMove; 

    [Header("가로 이동 설정 (상대 X 좌표)")]
    public float startX = 17f;
    public float endX = 4f;        
    public float moveDuration = 25f; // 속도를 더 늦춤 (20초 동안 이동)

    [Header("랜덤 주기")]
    public float minInterval = 10f;
    public float maxInterval = 20f;

    public Animator animator;

    void Start()
    {
        if (childToMove != null)
        {
            // 초기 위치를 가로 4.2로 설정
            childToMove.transform.localPosition = new Vector3(startX, childToMove.transform.localPosition.y, childToMove.transform.localPosition.z);
            StartCoroutine(StartAfterDelay());
        }
    }

    IEnumerator StartAfterDelay()
    {
        Debug.Log("게임 시작. 23초 대기 중...");
        yield return new WaitForSeconds(23f);

        while (true)
        {
            // 1. 랜덤 대기
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            animator.SetTrigger("code");

            // 2. 가로 이동 (4.2 -> 0.9)
            yield return StartCoroutine(MoveHorizontal(startX, endX));

            // 3. 도달 후 잠시 대기 (1초)
            yield return new WaitForSeconds(1f);

            // 4. 가로 복귀 (0.9 -> 4.2)
            yield return StartCoroutine(MoveHorizontal(endX, startX));
        }
    }

    IEnumerator MoveHorizontal(float fromX, float toX)
    {
        float elapsedTime = 0f;
        // 이동하는 동안 자식의 현재 Y, Z 좌표를 유지함
        float currentY = childToMove.transform.localPosition.y;
        float currentZ = childToMove.transform.localPosition.z;

        Vector3 startPos = new Vector3(fromX, currentY, currentZ);
        Vector3 endPos = new Vector3(toX, currentY, currentZ);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            
            // 아주 느리고 부드럽게 가로로 이동
            childToMove.transform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }

        childToMove.transform.localPosition = endPos;
    }
}