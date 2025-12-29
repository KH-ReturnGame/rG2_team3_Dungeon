using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleController : MonoBehaviour
{
    public float appearanceDelay = 1.2f;
    public float activeDuration = 1f;

    

    // 자식들을 담아둘 리스트
    private List<GameObject> childModels = new List<GameObject>();

    void Awake()
    {


        // 1. 부모 아래에 있는 모든 자식을 리스트에 담고 비활성화함
        foreach (Transform child in transform)
        {
            childModels.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }
    void Update()
    {

        
    }

    public void ReceiveSignal()
    {
        StopAllCoroutines();
        StartCoroutine(ObstacleSequence());
    }

    IEnumerator ObstacleSequence()
    {
        // 1.2초 대기
        yield return new WaitForSeconds(appearanceDelay);

        // 모든 자식 활성화
        SetAllChildren(true);

        // 1초 유지
        yield return new WaitForSeconds(activeDuration);

        // 모든 자식 비활성화
        SetAllChildren(false);
    }

    // 자식들의 상태를 한꺼번에 바꾸는 함수
    void SetAllChildren(bool state)
    {
        foreach (GameObject child in childModels)
        {
            if (child != null)
                child.SetActive(state);
        }
    }
}