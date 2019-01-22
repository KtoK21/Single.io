using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManage : MonoBehaviour
{

    public float Size;
    public float Speed;
    public string Name;

    private void Start()
    {
        if(transform.tag == "Heroparty")
        {
            Size = Random.Range(20, 40);
        }
    }
    private void Update()
    {
        //오브젝트가 용사일 경우
        if (transform.tag == "Heroparty")
        {

        }
        //AI 슬라임 혹은 플레이어 슬라임일 경우
        else
        {
            //슬라임의 크기를 현재 사이즈에 비례하게 설정
            //최대 크기 제한 => 50
            float slimeSize = Mathf.Clamp(1 + Size / 5, 1, 50);
            transform.localScale = new Vector3(slimeSize, slimeSize, slimeSize);

            //한 슬라임의 크기가 40이 되면 게임 오버(맵을 전부 덮음)
            if (Size == 50)
            {
                UIManage.IsGameOver = true;
            }
        }
    }
}

