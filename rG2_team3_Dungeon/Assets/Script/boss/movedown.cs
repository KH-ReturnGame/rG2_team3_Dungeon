using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 반드시 추가해야 함
using DG.Tweening;

public class ObjectManager : MonoBehaviour
{
    [Header("움직일 대상 (스크립트 없는 오브젝트)")]
    public boss_p mainboss;
    void Update()
    {
    
    }
    void Awake()
    {

    }
    // 씬이 로드될 때 실행되도록 이벤트를 연결함
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 이름이 "boss"일 때 실행함
        if (scene.name == "boss")
        {
            MoveTargetToSouthEnd();
        }
    }

    public void MoveTargetToSouthEnd()
    {

        this.transform.DOKill();
        
        // DOMove는 Vector3를 인자로 받으므로 new Vector3를 사용함
        // Y축만 -21로 이동하고 싶다면 DOMoveY를 사용함
        this.transform.DOMoveY(-21f, 1.0f);

    }
}