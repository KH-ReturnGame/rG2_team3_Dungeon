using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환 기능을 사용하기 위한 필수 네임스페이스

public class SceneLoader : MonoBehaviour
{
    // 버튼의 OnClick 이벤트에 연결될 공용 함수
    public void LoadNextScene(string sceneName)
    {
        // sceneName에 해당하는 씬을 로드합니다.
        // 이 함수가 실행되면 현재 씬은 종료되고 새 씬이 시작됩니다.
        SceneManager.LoadScene(sceneName); 
    }

    // 또는 빌드 인덱스를 사용할 수도 있습니다.
    // public void LoadNextSceneByIndex(int sceneIndex)
    // {
    //     SceneManager.LoadScene(sceneIndex);
    // }
}