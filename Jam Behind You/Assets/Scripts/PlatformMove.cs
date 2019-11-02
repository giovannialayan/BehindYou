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
    public bool reverse = true;
    public bool loop = false;
    private int currentIndex = 0;

    private Vector3 endPoint;
    private float fractionOfJourney;

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
        if(basicMovement == true)
        {
            if(loop == true)
            {
                reverse = false;
                int c = points.Count;
                Vector3 startPoint = points[0].transform.position;
                Vector3 currentPoint;
                Vector3 nextPoint;
                Vector3 endPoint;
                //
                //Debug.Log(startPoint);
                //Debug.Log(endPoint);

                if(currentIndex < (c - 1))
                {
                    currentPoint = points[currentIndex].transform.position;
                    nextPoint = points[currentIndex + 1].transform.position;

                    journeyLength = Vector3.Distance(currentPoint, nextPoint);

                    float distCovered = (Time.time - startTime) * speed;

                    fractionOfJourney = distCovered / journeyLength;

                    transform.position = Vector3.Lerp(currentPoint, nextPoint, fractionOfJourney);

                    if(transform.position == nextPoint)
                    {
                        currentIndex++;

                        startTime = Time.time;
                    }

                    Debug.Log(currentIndex);
                }
                else
                {
                    currentPoint = points[currentIndex].transform.position;
                    nextPoint = points[0].transform.position;

                    journeyLength = Vector3.Distance(currentPoint, nextPoint);

                    float distCovered = (Time.time - startTime) * speed;

                    fractionOfJourney = distCovered / journeyLength;

                    transform.position = Vector3.Lerp(currentPoint, nextPoint, fractionOfJourney);

                    if (transform.position == nextPoint)
                    {
                        currentIndex = 0;
                        startTime = Time.time;
                    }
                }
            }
            else if(reverse == true)
            {
                loop = false;
                //Vector3 startPoint = points[0].transform.position;
                //Vector3 endPoint = points[points.Count - 1].transform.position;
                //
                //Debug.Log(startPoint);
                //Debug.Log(endPoint);
                if(goingBackwards == false)
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        //Debug.Log(goingBackwards);
                        if (i < points.Count - 1)
                        {
                            journeyLength = Vector3.Distance(points[i].transform.position, points[i + 1].transform.position);

                            float distCovered = (Time.time - startTime) * speed;

                            fractionOfJourney = distCovered / journeyLength;

                            transform.position = Vector3.Lerp(points[i].transform.position, points[i + 1].transform.position, fractionOfJourney);
                        }
                    }
                    goingBackwards = true;
                }
                else if(goingBackwards == true)
                {
                    for (int i = points.Count - 1; i > 0; i--)
                    {
                        if (i > 0)
                        {
                            //Debug.Log(goingBackwards);
                            journeyLength = Vector3.Distance(points[i].transform.position, points[i - 1].transform.position);

                            float distCovered = (Time.time - startTime) * speed;

                            fractionOfJourney = distCovered / journeyLength;

                            transform.position = Vector3.Lerp(points[i].transform.position, points[i - 1].transform.position, fractionOfJourney);
                        }
                    }
                    goingBackwards = false;
                }
            }

            //transform.position = Vector3.Lerp(endPoint, startPoint, fractionOfJourney);

            /*if (Vector3.Distance(transform.position, points[1].transform.position) > 0 && !goingBackwards)
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
        */
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
