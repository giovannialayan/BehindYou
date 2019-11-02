using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public GameObject object1;
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = new Vector3(object1.transform.position.x, object1.transform.position.y, object1.transform.position.z - 5);
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector3(object1.transform.position.x, object1.transform.position.y, object1.transform.position.z - 5);
    }
}
