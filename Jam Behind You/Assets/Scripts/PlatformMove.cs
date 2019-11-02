using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    //basic movement variables
    public List<GameObject> points;
    public Vector3 pos;
    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;

    //size change variables
    private Vector3 originalSize;

    //platform type to be set in the inspector
    public bool basicMovement;
    public bool changeSize;

    //platform control
    private bool goingBackwards = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        originalSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //handle moving from point A to point B
        if (basicMovement)
        {
            //for (int i = 0; i < points.Count; i++)
            //{
            //    if (i <= points.Count - 1)
            //    {
            //        journeyLength = Vector3.Distance(points[i].transform.position, points[i + 1].transform.position);

            //        float distCovered = (Time.time - startTime) * speed;

            //        float fractionOfJourney = distCovered / journeyLength;

            //        transform.position = Vector3.Lerp(points[i].transform.position, points[i + 1].transform.position, fractionOfJourney);
            //    }
            //}

            if (Vector3.Distance(transform.position, points[1].transform.position) > 0 && !goingBackwards)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[1].transform.position, speed * Time.deltaTime);
            }
            else if(Vector3.Distance(transform.position, points[0].transform.position) > 0 && goingBackwards)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[0].transform.position, speed * Time.deltaTime);
            }
            else if (!goingBackwards)
            {
                goingBackwards = true;
            }
            else
            {
                goingBackwards = false;
            }
        }

        if (changeSize)
        {
            if(Vector3.Distance(transform.localScale, originalSize * .5f) > 0 && !goingBackwards)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, originalSize * .5f, speed * Time.deltaTime);
            }
            else if(Vector3.Distance(transform.localScale, originalSize) > 0 && goingBackwards)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, originalSize, speed * Time.deltaTime);
            }
            else if(!goingBackwards)
            {
                goingBackwards = true;
            }
            else
            {
                goingBackwards = false;
            }
        }
    }
}
