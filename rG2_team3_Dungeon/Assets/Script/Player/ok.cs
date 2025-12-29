using UnityEngine;

public class ok : MonoBehaviour
{

    void Awake()
    {
        // 이 오브젝트는 씬이 바뀌어도 파괴되지 않음
        DontDestroyOnLoad(gameObject);
    }
}