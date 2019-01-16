using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Generator : MonoBehaviour
{
    public Transform AISlimePrefab;
    public Transform PlayerSlimePrefab;
    public GameObject Background;

    //돌아다니면서 성장할 AI 슬라임들을 담은 리스트
    List<GameObject> AISlimes = new List<GameObject>();

    //가만히 있는 크기 1짜리 먹이 슬라임(플랑크톤)들을 담은 리스트
    List<GameObject> Planktons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
           
    }

    // AI슬라임들을 생성
    void AISlimeGenerator(int AICount)
    {
        for (int i = 0; i < AICount; i++)
        {
            //랜덤생성될 위치
            Vector3 RandomPos = new Vector3(3+i, 2+i, 0);

            //슬라임을 생성하고 리스트에 추가
            GameObject AISlime =Instantiate(AISlimePrefab, RandomPos, Quaternion.identity).gameObject;
            AISlimes.Add(AISlime);
            //생성된 슬라임에게 맵 사이즈를 알려줌
            AISlime.GetComponent<SlimeMove>().GetMapBound(Background.GetComponent<BoxCollider2D>().bounds.min, Background.GetComponent<BoxCollider2D>().bounds.max);



        }
    }
    
    //플레이어 슬라임을 생성
    void PlayerGenerator()
    {
        GameObject PlayerSlime = Instantiate(PlayerSlimePrefab, new Vector3(0, 0, 0), Quaternion.identity).gameObject;

        //플레이어 슬라임에게 맵 사이즈를 알려줌
        PlayerSlime.GetComponent<SlimeMove>().GetMapBound(Background.GetComponent<BoxCollider2D>().bounds.min, Background.GetComponent<BoxCollider2D>().bounds.max);
        //플레이어 슬라임에 붙은 카메라에게 맵 사이즈를 알려줌
        PlayerSlime.transform.GetChild(0).GetComponent<CameraControl>().GetMapBound(Background.GetComponent<BoxCollider2D>().bounds.min, Background.GetComponent<BoxCollider2D>().bounds.max);
    }

    void MapGenerator(int MapSize)
    {
        Background.transform.localScale = new Vector3(MapSize, MapSize, 1);

    }

    public void InitiateGame()
    {
        PlayerGenerator();
        AISlimeGenerator(GetComponent<DifficultyInfo>().AICount);
        MapGenerator(GetComponent<DifficultyInfo>().MapSize);
    }

}
