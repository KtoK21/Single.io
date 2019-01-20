using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 minBound;
    Vector3 maxBound;

    float halfHeight;
    float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        minBound = GameObject.Find("GameManager").GetComponent<Generator>().Background.GetComponent<BoxCollider2D>().bounds.min;
        maxBound = GameObject.Find("GameManager").GetComponent<Generator>().Background.GetComponent<BoxCollider2D>().bounds.max;

        halfHeight = GetComponent<Camera>().orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
        transform.position = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        float Xclamp = Mathf.Clamp(transform.parent.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);

        float Yclamp = Mathf.Clamp(transform.parent.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        transform.position = new Vector3(Xclamp, Yclamp, -10);

        //플레이어의 크기가 커질수록 카메라가 줌아웃.
        //최대 줌아웃은 15
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(1 + GetComponentInParent<SlimeManage>().Size * 0.5f, 1, 15);
    }

    public void SetMapBound(Vector3 minBoundparam, Vector3 maxBoundparam)
    {
        minBound = minBoundparam;
        maxBound = maxBoundparam;
    }
}
