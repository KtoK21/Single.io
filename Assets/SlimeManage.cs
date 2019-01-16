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

    public void Start()
    {
        
    }

    private void Update()
    {
        //슬라임의 크기를 현재 사이즈에 비례하게 설정
        transform.localScale = new Vector3(Size / 2, Size / 2, Size / 2);
    }

    //다른 오브젝트와 충돌했을 경우.
    //AI slime에도 적용되야하기 때문에, 자기들끼리 부딪혀도 실행되고 흡수-소멸 해야한다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Background")
        {
            float collisionSize = collision.transform.GetComponent<SlimeManage>().Size;

            //부딪힌 상대가 본인보다 크면
            if (collisionSize > Size)
            {
                //본인이 플레이어면 게임오버 UI 활성화
                if (transform.tag == "Player")
                {
                    UIManage.IsGameOver = true;
                }

                //본인이 AI면 오브젝트 삭제(임시) -> 파괴 애니메이션
                else
                    Destroy(gameObject);

            }

            //본인이 부딪힌 상대보다 크면
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
}
