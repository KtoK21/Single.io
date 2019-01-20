using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Generator : MonoBehaviour
{
    public Transform AISlimePrefab;
    public Transform PlayerSlimePrefab;
    public Transform PlanktonPrefab;
    public GameObject Background;

    //돌아다니면서 성장할 AI 슬라임들을 담은 리스트
    List<GameObject> AISlimes = new List<GameObject>();

    //가만히 있는 크기 1짜리 먹이 슬라임(플랑크톤)들을 담은 리스트
    List<GameObject> Planktons = new List<GameObject>();

    Vector3 minBound;
    Vector3 maxBound;
    // Start is called before the first frame update
    void Start()
    {
        //MapFix(GetComponent<DifficultyInfo>().MapSize);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Background.GetComponent<BoxCollider2D>().bounds.min);
        //맵에 생성된 총 플랑크톤의 갯수를 항상 일정하게 유지한다.
        if (SceneManager.GetActiveScene().name == "Ingame" && !UIManage.IsGameOver)
        {
            int PlanktonDiff = GetComponent<DifficultyInfo>().PlanktonCount - Planktons.Count;
            if (PlanktonDiff != 0)
                PlanktonGen(PlanktonDiff);
        }
    }

    // AI슬라임들을 생성. AICount 의 값만큼 새 AI슬라임을 생성한다.
    void AISlimeGen(int AICount)
    {
        for (int i = 0; i < AICount; i++)
        {

            //슬라임을 랜덤한 위치에 생성하고 리스트에 추가
            GameObject AISlime =Instantiate(AISlimePrefab, RandomPosGen(), Quaternion.identity).gameObject;
            AISlimes.Add(AISlime);
            //생성된 슬라임에게 맵 사이즈를 알려줌
            //AISlime.GetComponent<SlimeMove>().SetMapBound(minBound, maxBound);
        }
    }
    
    //플레이어 슬라임을 생성
    void PlayerGen()
    {
        GameObject PlayerSlime = Instantiate(PlayerSlimePrefab, new Vector3(0, 0, 0), Quaternion.identity).gameObject;

        //플레이어 슬라임에게 맵 사이즈를 알려줌
        //PlayerSlime.GetComponent<SlimeMove>().SetMapBound(Background.GetComponent<BoxCollider2D>().bounds.min, Background.GetComponent<BoxCollider2D>().bounds.max);
        //플레이어 슬라임에 붙은 카메라에게 맵 사이즈를 알려줌
        //PlayerSlime.transform.GetChild(0).GetComponent<CameraControl>().SetMapBound(minBound, maxBound);
    }

    //플랑크톤을 생성. PlanktonCount 의 값만큼 새 플랑크톤을 생성한다.
    void PlanktonGen(int PlanktonCount)
    {
        for (int i = 0; i < PlanktonCount; i++)
        {
            GameObject Plankton = Instantiate(PlanktonPrefab, RandomPosGen(), Quaternion.identity).gameObject;
            Plankton.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 255);
            Planktons.Add(Plankton);
        }
    }

    public void InitiateGame()
    {
        PlayerGen();
        AISlimeGen(GetComponent<DifficultyInfo>().AICount);
    }
    
    //Background의 범위 안에서 랜덤한 Vector3 생성. 벽에 딱 달라붙는 것을 제외하기 위해
    //min +1 ~ max -1 의 범위를 사용.
    Vector3 RandomPosGen()
    {
        
        Vector3 Pos = new Vector3(Random.Range(Background.GetComponent<BoxCollider2D>().bounds.min.x + 1, Background.GetComponent<BoxCollider2D>().bounds.max.x - 1), Random.Range(Background.GetComponent<BoxCollider2D>().bounds.min.y + 1, Background.GetComponent<BoxCollider2D>().bounds.max.y - 1), 0);

        return Pos;
    }

    public int GetPlanktonCount()
    {
        return Planktons.Count;
    }

    public void DestroyfromList(GameObject target, string type)
    {
        if (target != null)
        {
            if (type == "AISlime")
            {
                Destroy(target);
                if (AISlimes.Contains(target))
                {
                    AISlimes.Remove(target);
                }
            }
            else if (type == "Plankton")
            {
                Destroy(target);
                if (Planktons.Contains(target))
                {
                    Planktons.Remove(target);
                }
            }
        }
    }

    /*문제점 업데이트! 정확한 문제를 찾았다. Background 의 LocalScale을 바꾸고 나서
     * minBound, maxBound를 Background의 Boxcollider2D.bound의 min, max값으로 업데이트할때,
     * Background.GetComponent<BoxCollider2D>().bounds.min , max값이 업데이트되어있지 않다.
     * Update()에서도 같은 Debug.log를 출력하게 해보니, 함수 호출이 끝나고나서 한 프레임
     * 후부터 의도했던 결과값을 출력한다. 
     * 
     * localScale의 업데이트가 bound.min을 호출할때까지 끝나지 않아서 생기는 문제인듯.
     * MapFix 함수가 끝나고 한 프레임이 지날때까지 업데이트가 안된다.
     */


    // Background의 크기를 local scale로 조정. MapSize의 값만큼 scale을 늘린다.
    public void MapFix(int MapSize)
    {
        Background.transform.localScale = new Vector3(MapSize, MapSize, 1);

        Debug.Log(Background.GetComponent<BoxCollider2D>().bounds.min);

        minBound = Background.GetComponent < BoxCollider2D>().bounds.min;
        maxBound = Background.GetComponent<BoxCollider2D>().bounds.max;

        Debug.Log(Background.GetComponent<BoxCollider2D>().bounds.min);
    }

}
