using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DifficultyInfo : MonoBehaviour
{
    //AI 슬라임의 갯수, N => N개의 AI 슬라임
    public int AICount;
    //맵 사이즈, 1=>Scale (8,8,0), 2=>Scale (12,12,0), 3=>Scale(16,16,0), ...
    public int MapSize;
    //플랑크톤의 갯수, N => 언제나 게임에는 N개의 플랑크톤이 존재.
    public int PlanktonCount;

    private void Start()
    {
        AICount = 5;
        MapSize = 8;
        PlanktonCount = 10;
    }
}
