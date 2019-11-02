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

    //bezier curve variables
    private float count = 0;

    //platform type to be set in the inspector
    public bool basicMovement;
    public bool changeSize;
    public bool bezierCurve;

    //platform control
    private bool goingBackwards = false;
    private float speedTimer = 0;
    private float speedCooldown = 2;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        originalSize = transform.localScale;
        if(bezierCurve)
        {
            points[2] = new GameObject();
            points[2].transform.position = points[0].transform.position + (points[1].transform.position - points[0].transform.position) / 2 + Vector3.forward * 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //moving platform from point A to point B
        if (basicMovement)
        {
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

        //change size of platform
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

        //using a bezier curve algorithm to move a platform in a curve
        if (bezierCurve)
        {
            if(count < 1 && !goingBackwards)
            {
                count += Time.deltaTime;

                Vector3 slope1 = Vector3.Lerp(points[0].transform.position, points[2].transform.position, count);
                Vector3 slope2 = Vector3.Lerp(points[2].transform.position, points[1].transform.position, count);
                transform.position = Vector3.Lerp(slope1, slope2, count);
            }
            else if(count < 1 && goingBackwards)
            {
                count += Time.deltaTime;

                Vector3 slope1 = Vector3.Lerp(points[1].transform.position, points[2].transform.position, count);
                Vector3 slope2 = Vector3.Lerp(points[2].transform.position, points[0].transform.position, count);
                transform.position = Vector3.Lerp(slope1, slope2, count);
            }
            else if (!goingBackwards)
            {
                goingBackwards = true;
                count = 0;
            }
            else
            {
                goingBackwards = false;
                count = 0;
            }
        }

        if(speed < 1)
        {
            speedTimer += Time.deltaTime;
            if(speedTimer >= speedCooldown)
            {
                speedTimer = 0;
                speed = 1;
            }
        }
    }
}
