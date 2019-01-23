using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 슬라임 오브젝트에 동일한 스크립트를 달고, 플레이어가 조종하는 슬라임에 Player 태그를
//붙여 구분하는 식. 플레이어가 조종하냐 AI가 조종하냐의 차이를 제외하고는 로직이 완전히
//같아야 하기 때문.
public class SlimeMove : MonoBehaviour
{
    Vector3 minBound;
    Vector3 maxBound;

    Vector3 Destination;

    public bool IsTargeting = false;

    // Start is called before the first frame update
    void Start()
    {
        Destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManage.IsGameOver && !UIManage.IsGameClear)
        {
            //플레이어 슬라임이면 키보드 이동 함수를  실행
            if (transform.tag == "Player")
            {
                Destination = PlayerDestination();
                PlayerMove(Destination);
            }
            else
            {
                if (transform.position == Destination)
                {
                    IsTargeting = false;
                    Destination = RandomDestination();
                }
                AIMove(Destination);
            }
        }
    }

    void PlayerMove(Vector3 _Destination)
    {
        //맵 안벗어나게 Clamp
        transform.position += _Destination * GetComponent<SlimeManage>().Speed * Time.deltaTime;

        float Xclamp = Mathf.Clamp(transform.position.x, minBound.x, maxBound.x);
        float Yclamp = Mathf.Clamp(transform.position.y, minBound.y, maxBound.y);

        transform.position = new Vector3(Xclamp, Yclamp, 0);
    }

    void AIMove(Vector3 _Destination)
    {
        transform.position = Vector3.MoveTowards(transform.position, _Destination, GetComponent<SlimeManage>().Speed * Time.deltaTime);

        float Xclamp = Mathf.Clamp(transform.position.x, minBound.x, maxBound.x);
        float Yclamp = Mathf.Clamp(transform.position.y, minBound.y, maxBound.y);

        transform.position = new Vector3(Xclamp, Yclamp, 0);
    }

    Vector3 PlayerDestination()
    {
        

        return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
    }

    Vector3 RandomDestination()
    {
        return new Vector3(Random.Range(minBound.x + 1, maxBound.x - 1), Random.Range(minBound.y + 1, maxBound.y - 1), 0);
    }

    //전달받은 목표위치를 Clamp한 다음 Destination에 저장.
    public void SetDestination(Vector3 param)
    {
        float Xclamp = Mathf.Clamp(param.x, minBound.x, maxBound.x);
        float Yclamp = Mathf.Clamp(param.y, minBound.y, maxBound.y);

        Destination = new Vector3(Xclamp, Yclamp, 0);
    }

    public void SetMapBound(Vector3 minBoundparam, Vector3 maxBoundparam)
    {
        minBound = minBoundparam;
        maxBound = maxBoundparam;
    }
}