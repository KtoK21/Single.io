using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject targetBackground;

    Vector3 minBound;
    Vector3 maxBound;

    float halfHeight;
    float halfWidth;

    // Start is called before the first frame update
    void Start()
    {

        minBound = targetBackground.GetComponent<BoxCollider2D>().bounds.min;
        maxBound = targetBackground.GetComponent<BoxCollider2D>().bounds.max;
        halfHeight = transform.GetComponent<Camera>().orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        float Xclamp = Mathf.Clamp(transform.parent.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);

        float Yclamp = Mathf.Clamp(transform.parent.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        transform.position = new Vector3(Xclamp, Yclamp, -10);
    }   
}
