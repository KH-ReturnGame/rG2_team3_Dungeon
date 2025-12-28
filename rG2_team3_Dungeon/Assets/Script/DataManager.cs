using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public int gold = 1000; // 현재 보유 금액

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 이 오브젝트는 씬이 바뀌어도 안 사라짐
        }
        else
        {
            Destroy(gameObject);
        }
    }
}