using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyInfo : MonoBehaviour
{
    //AI 슬라임의 갯수, N => N개의 AI 슬라임
    public int AICount;
    //맵 사이즈, 1=>Scale (4,4,0), 2=>Scale (6,6,0), 3=>Scale(8,8,0), ...
    public int MapSize;

    private void Start()
    {
        AICount = 5;
        MapSize = 4;
    }

    public void GetAICount(int param)
    {
        Debug.Log(param);
        AICount = (param + 1) * 5;
    }
    
    public void GetMapSize(int param)
    {
        Debug.Log(param);
        MapSize = (param * 2) + 4;
    }
}
