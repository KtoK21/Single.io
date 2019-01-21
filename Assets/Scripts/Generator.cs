using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Generator : MonoBehaviour
{
    public Transform AISlimePrefab;
    public Transform PlayerSlimePrefab;
    public Transform HeroPartyPrefab;
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
        //minBound와 maxBound의 초기값을 Background의 초기 bounds.min, max로 설정함.
        minBound = Background.GetComponent<BoxCollider2D>().bounds.min;
        maxBound = Background.GetComponent<BoxCollider2D>().bounds.max;
    }

    // Update is called once per frame
    void Update()
    {

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

            AISlime.GetComponent<SlimeMove>().SetMapBound(minBound, maxBound);
        }
    }
    
    //플레이어 슬라임을 생성
    void PlayerGen()
    {
        GameObject PlayerSlime = Instantiate(PlayerSlimePrefab, new Vector3(0, 0, 0), Quaternion.identity).gameObject;

        //플레이어 슬라임에게 맵 사이즈를 알려줌
        PlayerSlime.GetComponent<SlimeMove>().SetMapBound(minBound, maxBound);
        //플레이어 슬라임에 붙은 카메라에게 맵 사이즈를 알려줌
        
        PlayerSlime.transform.GetChild(0).GetComponent<CameraControl>().SetMapBound(minBound, maxBound);
    }

    void HeroPartyGen()
    {
        GameObject HeroParty = Instantiate(HeroPartyPrefab, RandomPosGen(), Quaternion.identity).gameObject;

        //플레이어 슬라임에게 맵 사이즈를 알려줌
       HeroParty.GetComponent<SlimeMove>().SetMapBound(minBound, maxBound);
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
        HeroPartyGen();
    }
    
    //Background의 범위 안에서 랜덤한 Vector3 생성. 벽에 딱 달라붙는 것을 제외하기 위해
    //min +1 ~ max -1 의 범위를 사용.
    Vector3 RandomPosGen()
    {
        
        Vector3 Pos = new Vector3(Random.Range(minBound.x + 1, maxBound.x - 1), Random.Range(minBound.y + 1, maxBound.y - 1), 0);

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


    /* Background의 크기를 local scale로 조정. MapSize의 값만큼 scale을 늘린다.
     * 원래는 transform.localScale을 바꾸면, Background에 붙어있는 BoxCollider2D의
     * 바운더리 범위도 알아서 바뀌어야 하는데, 이게 약간 늦게 업데이트 되는 것 같다.
     * 그래서 그냥 minBound = BoxCollider2D.bounds.min 을 하면 scale이 증가되기 전의 값을
     * 가져오는 문제가 생겨서, 직접 값을 계산해서 넣는 식으로 해결했다.
     */

    // Background의 크기를 local scale로 조정. MapSize의 값만큼 scale을 늘린다.
    public void MapFix(int newMapSize)
    { 
        float prevMapSize = Background.transform.localScale.x;

        Background.transform.localScale = new Vector3(newMapSize, newMapSize, 1);

        minBound = minBound * newMapSize / prevMapSize;
        maxBound = maxBound * newMapSize / prevMapSize;

    }

}
