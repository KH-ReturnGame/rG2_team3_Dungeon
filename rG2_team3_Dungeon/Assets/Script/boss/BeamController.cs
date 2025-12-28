using UnityEngine;
using System.Collections;

public class BeamController : MonoBehaviour
{
    [Header("설정")]
    public float beamDelay = 2f;    // 머리 생성 후 빔이 나올 때까지 대기 시간
    public float beamDuration = 1f; // 빔이 유지되는 시간

    private GameObject beamObject;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("beam"))
            {
                beamObject = child.gameObject;
                break;
            }
        }

        // 시작 시 빔은 꺼둠
        if (beamObject != null)
            beamObject.SetActive(false);
    }

    // 부모(ObstacleController)가 이 오브젝트를 활성화할 때 자동으로 실행됨
    void OnEnable()
    {
        if (beamObject != null)
        {
            StopAllCoroutines();
            StartCoroutine(BeamSequence());
        }
    }

    IEnumerator BeamSequence()
    {
        // 1. 빔 오브젝트 초기화 (꺼진 상태 확인)
        beamObject.SetActive(false);

        // 2. 1초 대기 (머리가 소환되어 기를 모으는 시간)
        yield return new WaitForSeconds(beamDelay);

        // 3. 빔 활성화 (발사!)
        beamObject.SetActive(true);
        Debug.Log("빔 발사!");

        // 4. 유지 시간 후 빔 비활성화
        yield return new WaitForSeconds(beamDuration);
        beamObject.SetActive(false);
    }
}