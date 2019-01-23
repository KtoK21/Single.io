using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//주변을 감지하는 collider와 실제 충돌을 감지하는 collider를 따로 두고, 각 collider의 Trigger call이 또다른 collider의 Trigger call을 하지 않도록 스크립트를 따로 만들었다.
public class SlimeContactManage : MonoBehaviour
{
    //다른 오브젝트와 충돌했을 경우.
    //AI slime에도 적용되야하기 때문에, 자기들끼리 부딪혀도 실행되고 흡수-소멸 해야한다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //백그라운드 혹은 다른 슬라임의 DetectCollider와 충돌하는 것을 무시.
        if (collision.tag != "Background" && collision.name != "DetectCollider")
        {
            //플랑크톤에 닿으면
            if (collision.tag == "Plankton")
            {
                //플랑크톤을 리스트에서 지우고 파괴하며, 본인 사이즈 + 1
                GameObject.Find("GameManager").GetComponent<Generator>().DestroyfromList(collision.transform.parent.gameObject, "Plankton");
                transform.parent.GetComponent<SlimeManage>().Size += 1;
            }

            //부딪힌 상대가 본인보다 크면 (잡아먹힘)
            else if (collision.transform.parent.GetComponent<SlimeManage>().Size > transform.parent.GetComponent<SlimeManage>().Size)
            {
                //본인이 플레이어면 게임오버 UI 활성화
                if (transform.tag == "Player")
                {
                    GameObject.Find("GameManager").GetComponent<UIManage>().GameOver();
                }
                //본인이 용사면
                else if (transform.tag == "Heroparty")
                {
                    //플레이어가 잡아먹을 경우 게임 클리어
                    if (collision.tag == "Player")
                        GameObject.Find("GameManager").GetComponent<UIManage>().GameClear();
                    //AI가 잡아먹을 경우 게임 오버
                    else
                        GameObject.Find("GameManager").GetComponent<UIManage>().GameOver();
                }

                //본인이 AI면 오브젝트 삭제
                else
                    GameObject.Find("GameManager").GetComponent<Generator>().DestroyfromList(transform.parent.gameObject, "AISlime");

            }

            //본인이 부딪힌 상대보다 크면 (잡아먹음)
            else if (collision.transform.parent.GetComponent<SlimeManage>().Size < transform.parent.GetComponent<SlimeManage>().Size)
            {
                //본인의 사이즈에 상대의 사이즈를 더한다.
                transform.parent.GetComponent<SlimeManage>().Size += collision.transform.parent.GetComponent<SlimeManage>().Size;
            }

            //본인과 부딪힌 상대의 크기가 같으면
            else
            {
                //튕겨나온다? 둘다 죽는다? 1/2 확률이다? 안정함
            }
        }
    }

}
