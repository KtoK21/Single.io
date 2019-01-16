using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManage : MonoBehaviour
{

    public static bool IsGameOver = false;
    public static bool IsGenerating = false;

    public GameObject Canvas;
    public GameObject HomeUI;
    public GameObject IngameUI;

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


    }

    public void StartGame()
    {
        //HomeUI 비활성화 후 Ingame Scene 로드
        HomeUI.SetActive(!HomeUI.activeSelf);

        StartCoroutine(StartGameCoroutine());

  
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
}
