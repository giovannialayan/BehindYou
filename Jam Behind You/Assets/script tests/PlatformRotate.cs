using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    public bool rotate = true;
    public float RX;
    public float RY;
    public float RZ;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotate == true)
        {
            transform.Rotate(RX, RY, RZ);
        }

    }
}
