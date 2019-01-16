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
        GetComponent<Camera>().orthographicSize = 1 + GetComponentInParent<SlimeManage>().Size *1.5f;
    }

    public void GetMapBound(Vector3 minBoundparam, Vector3 maxBoundparam)
    {
        minBound = minBoundparam;
        maxBound = maxBoundparam;
    }
}
