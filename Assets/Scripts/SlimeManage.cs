using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManage : MonoBehaviour
{

    public float Size;
    public float Speed;
    public string Name;

    public SlimeManage()
    {
        Size = 2.0f;
        Speed = 5.0f;
        Name = "Test";
    }

    private void Update()
    {
        //슬라임의 크기를 현재 사이즈에 비례하게 설정
        //최대 크기 제한 => 40
        float slimeSize = Mathf.Clamp(1 + Size / 5, 1, 40);
        transform.localScale = new Vector3(slimeSize, slimeSize, slimeSize);

        //한 슬라임의 크기가 40이 되면 게임 오버(맵을 전부 덮음)
        if(Size == 40)
        {
            UIManage.IsGameOver = true;
        }
    }

    //다른 오브젝트와 충돌했을 경우.
    //AI slime에도 적용되야하기 때문에, 자기들끼리 부딪혀도 실행되고 흡수-소멸 해야한다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //백그라운드에 있는 Collider2D에 충돌하는 것을 무시.
        if (collision.tag == "Background")
            return;

        else if(collision.tag == "Plankton")
        {
            GameObject.Find("GameManager").GetComponent<Generator>().DestroyfromList(collision.gameObject, "Plankton");
            Size += 1;
            return;
        }

        float collisionSize = collision.transform.GetComponent<SlimeManage>().Size;
        
        //부딪힌 상대가 본인보다 크면 (잡아먹힘)
        if (collisionSize > Size)
        {
            //본인이 플레이어면 게임오버 UI 활성화
            if (transform.tag == "Player")
            {
                UIManage.IsGameOver = true;
            }

            //본인이 AI면 오브젝트 삭제
            else
                GameObject.Find("GameManager").GetComponent<Generator>().DestroyfromList(gameObject, "AISlime");

        }

        //본인이 부딪힌 상대보다 크면 (잡아먹음)
        else if (collisionSize < Size)
        {
            //본인의 사이즈에 상대의 사이즈를 더한다.
            Size += collisionSize;
        }

        //본인과 부딪힌 상대의 크기가 같으면
        else
        {
            //튕겨나온다? 둘다 죽는다? 1/2 확률이다? 안정함
        }
    }
}

