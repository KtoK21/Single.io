using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class SlimeInfo : MonoBehaviour
{

    public float Size { get; set; }
    public float Speed { get; set; }
    public string Name { get; set; }

    public SlimeInfo()
    {
        Size = 2.0f;
        Speed = 5.0f;
        Name = "Test";
    }

}
