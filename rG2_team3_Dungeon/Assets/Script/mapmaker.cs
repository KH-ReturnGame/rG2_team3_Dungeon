using UnityEngine;

public class NonOverlappingEdgeSpawner : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public Transform object3;
    public Transform object4;

    public GameObject objectToSpawn;
    public int numberOfObjectsPerSide = 5;
    public float sexmax;//x범위 최대
    public float seymay;//y범위 최대
    public float sexmin;//x범위 최소
    public float seymin;//y범위 최소
    public int count = 30;//오브젝트 생성 개수



    public void start()
    {
        sexmax = 22f;
        sexmin = -19f;
        seymay = 11f;
        seymin = -11f;
        //오브젝트 위치로 바꿀 예정임 ㅇㅇ
        while (/*범위 설정*/)
        {
            float targetX = Random.Range(-19f, 22f);
            if (target)
            {
                Vector3 spawnPosition = new Vector3(targetX, targetY, targetZ);

            }
        }
        
    }

    
}