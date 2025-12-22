// FloatRange.cs 파일 내용
using UnityEngine;
using System; // [Serializable]을 사용할 경우 using System이 명시되어 있는지 확인합니다.

[Serializable] // 반드시 [System.Serializable] 또는 [Serializable] 속성이 있어야 합니다.
public struct FloatRange
{
    public float min;
    public float max;

    public float GetRandom()
    {
        // Random.Range는 max 미만을 반환하지만, 
        // Unity의 Random.Range(float, float)는 max를 포함합니다.
        return UnityEngine.Random.Range(min, max); 
    }
}