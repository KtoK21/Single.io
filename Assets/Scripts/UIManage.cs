using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManage : MonoBehaviour
{

    public static bool IsGameOver = false;
    public static bool IsGenerating = false;

    public GameObject Canvas;
    public GameObject HomeUI;
    public GameObject IngameUI;
    public GameObject DebugUI;

    public Text PlanktonCountText;

    private void Awake()
    {
        //GameManager는 파괴하지 않고 여러 씬들 사이에서 하나로 계속 작동.
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
            IngameUI.SetActive(true);
        }

        PlanktonCountText.text = GetComponent<Generator>().GetPlanktonCount().ToString();

    }

    public void StartGame()
    {
        //HomeUI 비활성화 후 Ingame Scene 로드
        HomeUI.SetActive(!HomeUI.activeSelf);
        DebugUI.SetActive(!DebugUI.activeSelf);

        StartCoroutine(StartGameCoroutine());
        
  
    }

    /*문제점! 이 코루틴에서 마지막의 InitiateGame()이 먼저 실행되고 그 윗줄의
     * Debug.Log가 실행된다. 뭐지?????
     */

    IEnumerator StartGameCoroutine()
    {
        SceneManager.LoadScene("Ingame");
        while (!SceneManager.GetSceneByName("Ingame").isLoaded)
            yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Ingame"));
        Debug.Log("Ingame loaded & active");
        GetComponent<Generator>().InitiateGame();
    }

    public void GetAICount(int param)
    {
        GetComponent<DifficultyInfo>().AICount = (param + 1) * 5;
    }

    public void GetMapSize(int param)
    {
        int MapSize = (param * 4) + 8;
        GetComponent<DifficultyInfo>().MapSize = MapSize;
        GetComponent<Generator>().MapFix(MapSize);
    }

    public void GetPlanktonCount(Slider slider)
    {

        GetComponent<DifficultyInfo>().PlanktonCount = (int)slider.value;
        slider.transform.Find("CountText").GetComponent<Text>().text = slider.value.ToString();
    }
}
