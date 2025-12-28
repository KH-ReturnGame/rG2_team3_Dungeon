using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_forest : MonoBehaviour
{
    public string nextSceneName;
    public Vector3 spawnPosition; //forest씬 시작 좌표

    public GameObject forestmove; // 화면에 띄울 UI 오브젝트
    private bool isPlayerNearby = false;

    void Start()
    {
        // 시작할 때 UI를 숨김
        if (forestmove != null) forestmove.SetActive(false);
    }

    void Update()
    {
        // 플레이어가 근처에 있고 Space 키를 누르면 실행
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.instance.nextSpawnPoint = spawnPosition;
            Debug.Log("아카네리제 최고다!!");
            SceneManager.LoadScene("forest");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 태그를 확인하여 UI 활성화
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            forestmove.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            // interactionUI가 아직 존재하는지(파괴되지 않았는지) 확인
            if (forestmove != null) 
            {
                forestmove.SetActive(false);
            }
        }
    }
}