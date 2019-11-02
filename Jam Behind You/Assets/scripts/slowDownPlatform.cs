using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowDownPlatform : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);
                hit.collider.GetComponent<PlatformMove>().speed = .2f;
            }
        }
    }
}
