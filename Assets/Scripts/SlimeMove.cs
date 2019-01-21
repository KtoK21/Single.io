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

  
    // Start is called before the first frame update
    void Start()
    {
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
    }

    void PlayerMove()
    {
        //맵 안벗어나게 Clamp
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * GetComponent<SlimeManage>().Speed * Time.deltaTime;

        float Xclamp = Mathf.Clamp(transform.position.x, minBound.x, maxBound.x);
        float Yclamp = Mathf.Clamp(transform.position.y, minBound.y, maxBound.y);

        transform.position = new Vector3(Xclamp, Yclamp, 0);
    }

    public void SetMapBound(Vector3 minBoundparam, Vector3 maxBoundparam)
    {
        minBound = minBoundparam;
        maxBound = maxBoundparam;
    }
}