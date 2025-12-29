using UnityEngine;
using System.Collections;

public class boom : MonoBehaviour
{
    public float appearanceDelay = 14f; // 신호 후 등장까지 대기
    public float laserDelay = 1.0f;      // 머리 나온 후 빔 나올 때까지 대기
    public float activeDuration = 1.5f;  // 빔 유지 시간

    private GameObject head;
    public GameObject beam;

    //public Animator animator;

    void Awake()
    {
        // 1. 첫 번째 자식(sexybones_0)을 가져옴
        if (transform.childCount > 0)
        {
            head = transform.GetChild(0).gameObject;

            // 2. 그 자식의 첫 번째 자식(bim/beam/Square)을 가져옴
            if (head.transform.childCount > 0)
            {
                beam = head.transform.GetChild(0).gameObject;
            }
        }

        // 초기 상태는 둘 다 꺼둠
        if (head != null) head.SetActive(false);
        if (beam != null) beam.SetActive(false);
        ReceiveSignal();
    }

    public void ReceiveSignal()
    {
        StopAllCoroutines();
        StartCoroutine(ObstacleSequence());
    }

    IEnumerator ObstacleSequence()
    {
        // 1. 소환 대기
        yield return new WaitForSeconds(appearanceDelay);

        // 2. 머리(sexybones_0) 활성화
        if (head != null)
        {
            head.SetActive(true);
            // 빔은 아직 꺼진 상태여야 함
            if (beam != null) beam.SetActive(false); 
        }

        // 3. 1초 대기 (기 모으는 연출)
        yield return new WaitForSeconds(laserDelay);

        // 4. 빔(bim) 활성화
        if (beam != null)
        {
            beam.SetActive(true);
            Debug.Log("빔 공격 시작!");
            //animator.SetTrigger("a");
        }

        // 5. 유지 시간 후 모두 종료
        yield return new WaitForSeconds(activeDuration);

        if (head != null) head.SetActive(false);
        // 부모가 꺼지면 자식인 빔도 같이 꺼짐
    }

    //void Go(){
    //    beam.SetActive(true);
    //}

    //void stop(){
    //    beam.SetActive(false);
    //}
}