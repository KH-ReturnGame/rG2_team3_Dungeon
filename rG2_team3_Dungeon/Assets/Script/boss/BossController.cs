using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    // 에디터에서 장애물 오브젝트를 드래그해서 연결
    public ObstacleController obstacle; 

    void Start()
    {
        obstacle.ReceiveSignal();
    }
    void Update()
    {
        // 예: 스페이스바를 누르면 장애물 등장
        if (Input.GetKeyDown(KeyCode.Space))
        {
            obstacle.ReceiveSignal();
        }
    }
}