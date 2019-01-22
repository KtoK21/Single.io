using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDetectManage : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.transform != null && collision.tag != "Background")
        {
            //감지된 슬라임의 크기가 자신보다 크면 도망. 먹이를 포착해 쫓아가다가도 도망을 우선시 해야한다.
            if (collision.tag != "Plankton" && collision.transform.parent.GetComponent<SlimeManage>().Size > transform.parent.GetComponent<SlimeManage>().Size)
            {
                Vector3 dirToRun = transform.position - collision.transform.position;
                Vector3 Destination = transform.position + dirToRun;

                //도망가는 각도 랜덤부여(테스트 다시! 별로 효과가 없다)
                Destination = new Vector3(Destination.x + Random.Range(-2, 2), Destination.y + Random.Range(-2, 2), 0);

                transform.parent.GetComponent<SlimeMove>().SetDestination(Destination);
            }
            //이미 목표위치가 정해져 있지 않고, 도망치는 상태가 아니며, 감지된 슬라임이 플랑크톤이거나 감지된 슬라임의 크기가 자신보다 작으면 추적
            //&& !transform.parent.GetComponent<SlimeMove>().IsRunning
            else if (!transform.parent.GetComponent<SlimeMove>().IsTargeting  && (collision.tag == "Plankton" || collision.transform.parent.GetComponent<SlimeManage>().Size < transform.parent.GetComponent<SlimeManage>().Size))
            {
                //추적하는 상태로 돌입
                transform.parent.GetComponent<SlimeMove>().IsTargeting = true;
                transform.parent.GetComponent<SlimeMove>().SetDestination(collision.transform.position);
            }

        }
    }
}
