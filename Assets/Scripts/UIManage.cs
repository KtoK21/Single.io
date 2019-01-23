using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManage : MonoBehaviour
{

    public static bool IsGameOver = false;
    public static bool IsGenerating = false;
    public static bool IsGameClear = false;
    public GameObject Canvas;
    public GameObject HomeUI;
    public GameObject IngameUI;
    public GameObject DebugUI;

    public AudioSource GameOverAudio;
    public AudioSource GameClearAudio;
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
        PlanktonCountText.text = GetComponent<Generator>().GetPlanktonCount().ToString();
    }

    void StartGame()
    {
        //HomeUI 비활성화 후 Ingame Scene 로드
        HomeUI.SetActive(!HomeUI.activeSelf);
        DebugUI.SetActive(!DebugUI.activeSelf);

        StartCoroutine(StartGameCoroutine());
    }

    public void GameOver()
    {
        IsGameOver = true;
        IngameUI.transform.Find("GameOver").gameObject.SetActive(true);
        IngameUI.transform.Find("RestartButton").gameObject.SetActive(true);
        GameOverAudio.Play();
    }

    public void GameClear()
    {
        IsGameClear = true;
        IngameUI.transform.Find("GameClear").gameObject.SetActive(true);
        IngameUI.transform.Find("RestartButton").gameObject.SetActive(true);
        GameClearAudio.Play();
    }

    IEnumerator StartGameCoroutine()
    {
        SceneManager.LoadScene("Ingame");
        while (!SceneManager.GetSceneByName("Ingame").isLoaded)
            yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Ingame"));
        Debug.Log("Ingame loaded & active");
        GetComponent<Generator>().InitiateGame();
    }

    public void SetAICount(int param)
    {
        GetComponent<DifficultyInfo>().AICount = (param + 1) * 5;
    }

    public void SetMapSize(int param)
    {
        int MapSize = (param * 4) + 8;
        GetComponent<DifficultyInfo>().MapSize = MapSize;
        GetComponent<Generator>().MapFix(MapSize);
    }

    public void SetPlanktonCount(Slider slider)
    {
        GetComponent<DifficultyInfo>().PlanktonCount = (int)slider.value;
        slider.transform.Find("CountText").GetComponent<Text>().text = slider.value.ToString();
    }
    void DebugTimex5()
    {
        GetComponent<Generator>().SetSlimeSpeed(25);
    }

    public void OnRestart()
    {
        foreach(Transform child in IngameUI.transform)
        {
            child.gameObject.SetActive(false);
        }

        IsGameOver = false;
        IsGameClear = false;

        SceneManager.LoadScene("Home");
    }
}
