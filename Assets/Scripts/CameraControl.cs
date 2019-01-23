using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 minBound;
    Vector3 maxBound;

    float halfHeight;
    float halfWidth;

    float Xclamp;
    float Yclamp;

    // Start is called before the first frame update
    void Start()
    {
        halfHeight = GetComponent<Camera>().orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
        transform.position = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        Xclamp = Mathf.Clamp(transform.parent.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);

        Yclamp = Mathf.Clamp(transform.parent.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        transform.position = new Vector3(Xclamp, Yclamp, -10);

        //플레이어의 크기가 커질수록 카메라가 줌아웃.
        //최대 줌아웃은 15
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(1 + GetComponentInParent<SlimeManage>().Size, 1, 15);
    }

    public void SetMapBound(Vector3 minBoundparam, Vector3 maxBoundparam)
    {
        minBound = minBoundparam;
        maxBound = maxBoundparam;
    }
}
