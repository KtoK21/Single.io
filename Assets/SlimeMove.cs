using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//모든 슬라임 오브젝트에 동일한 스크립트를 달고, 플레이어가 조종하는 슬라임에 Player 태그를
//붙여 구분하는 식. 플레이어가 조종하냐 AI가 조종하냐의 차이를 제외하고는 로직이 완전히
//같아야 하기 때문.
public class SlimeMove : MonoBehaviour
{
    public GameObject targetBackground;
    public float Size = 2.0f;   //사이즈 인스펙터에서 조절하기 위해
    SlimeInfo SlimeInfo;        //각 슬라임 오브젝트마다 정보 저장
    Vector3 minBound;
    Vector3 maxBound;


    // Start is called before the first frame update
    void Start()
    {
        SlimeInfo = GetComponent<SlimeInfo>();
        SlimeInfo.Size = Size;

        minBound = targetBackground.GetComponent<BoxCollider2D>().bounds.min;
        maxBound = targetBackground.GetComponent<BoxCollider2D>().bounds.max;
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 슬라임이면 키보드 이동 함수를  실행
        if (transform.tag == "Player")      
            PlayerMove();

        else
        {
            //AIMove();
        }

        //슬라임의 사이즈를 localScale로 조절(임시). SlimeInfo의 사이즈에 비례.
        transform.localScale = new Vector3(SlimeInfo.Size / 2, SlimeInfo.Size / 2, SlimeInfo.Size / 2);

        
    }

    void PlayerMove()
    {

        //맵 안벗어나게 Clamp

        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) *  SlimeInfo.Speed * Time.deltaTime;

        float Xclamp = Mathf.Clamp(transform.position.x, minBound.x, maxBound.x);
        float Yclamp = Mathf.Clamp(transform.position.y, minBound.y, maxBound.y);


        transform.position = new Vector3(Xclamp, Yclamp, 0);
    }

    //다른 오브젝트와 충돌했을 경우.
    //AI slime에도 적용되야하기 때문에, 자기들끼리 부딪혀도 실행되고 흡수-소멸 해야한다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Background")
        {
            float collisionSize = collision.transform.GetComponent<SlimeInfo>().Size;

            //부딪힌 상대가 본인보다 크면
            if (collisionSize > SlimeInfo.Size)
            {
                //본인이 플레이어면 게임오버 UI 활성화
                if (transform.tag == "Player")
                {
                    GameObject.Find("GameManager").GetComponent<UIManage>().IsGameOver = true;
                }

                //본인이 AI면 오브젝트 삭제(임시) -> 파괴 애니메이션
                else
                    Destroy(gameObject);

            }

            //본인이 부딪힌 상대보다 크면
            else if(collisionSize < SlimeInfo.Size)
            {
                //본인의 사이즈에 상대의 사이즈를 더한다.
                SlimeInfo.Size += collisionSize;
            }
        }
    }
}