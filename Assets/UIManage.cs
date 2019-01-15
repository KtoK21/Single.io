using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManage : MonoBehaviour
{
    public bool IsGameOver = false;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
            Canvas.SetActive(true);
        }   
    }
}
