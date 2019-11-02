using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public List<GameObject> points;
    public Vector3 pos;
    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < points.Count; i++)
        {
            if(i <= points.Count - 1)
            {
                journeyLength = Vector3.Distance(points[i].transform.position, points[i + 1].transform.position);

                float distCovered = (Time.time - startTime) * speed;

                float fractionOfJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(points[i].transform.position, points[i + 1].transform.position, fractionOfJourney);
            }
        }
    }
}
