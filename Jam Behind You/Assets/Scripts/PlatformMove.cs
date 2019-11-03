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

    //platform flip variables
    private float flipCooldown = 0;
    private bool shouldFlip = true;

    //platform type to be set in the inspector
    public bool basicMovement;
    public bool changeSize;
    public bool bezierCurve;
    public bool flipPlatform;

    //platform control
    private bool goingBackwards = false;
    public bool reverse = false;
    public bool loop = false;
    private int currentIndex = 0;

    private Vector3 endPoint;
    private float fractionOfJourney;

    //platform slow down variables
    private float speedTimer = 0;
    private float speedCooldown = 2;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        originalSize = transform.localScale;
        if (bezierCurve)
        {
            points[2] = new GameObject();
            points[2].transform.position = points[0].transform.position + (points[1].transform.position - points[0].transform.position) / 2 + Vector3.forward * 5;
        }
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
                //Vector3 startPoint = points[0].transform.position;
                Vector3 currentPoint;
                Vector3 nextPoint;
                //Vector3 endPoint;
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

                int c = points.Count;
                //Vector3 startPoint = points[0].transform.position;
                Vector3 currentPoint;
                Vector3 nextPoint;
                //Vector3 startPoint = points[0].transform.position;
                //Vector3 endPoint = points[points.Count - 1].transform.position;
                //
                //Debug.Log(startPoint);
                //Debug.Log(endPoint);
                if (goingBackwards == false)
                {
                    if (currentIndex < (c - 1))
                    {
                        currentPoint = points[currentIndex].transform.position;
                        nextPoint = points[currentIndex + 1].transform.position;

                        journeyLength = Vector3.Distance(currentPoint, nextPoint);

                        float distCovered = (Time.time - startTime) * speed;

                        fractionOfJourney = distCovered / journeyLength;

                        transform.position = Vector3.Lerp(currentPoint, nextPoint, fractionOfJourney);

                        if (transform.position == nextPoint)
                        {
                            currentIndex++;

                            startTime = Time.time;
                        }
                    }
                    else
                    {
                        goingBackwards = true;
                    }
                }
                else if(goingBackwards == true)
                {
                    if (currentIndex > 0)
                    {
                        currentPoint = points[currentIndex].transform.position;
                        nextPoint = points[currentIndex - 1].transform.position;

                        journeyLength = Vector3.Distance(currentPoint, nextPoint);

                        float distCovered = (Time.time - startTime) * speed;

                        fractionOfJourney = distCovered / journeyLength;

                        transform.position = Vector3.Lerp(currentPoint, nextPoint, fractionOfJourney);

                        if (transform.position == nextPoint)
                        {
                            currentIndex--;

                            startTime = Time.time;
                        }

                        Debug.Log(currentIndex);
                    }
                    else
                    {
                        goingBackwards = false;
                    }
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
                count += Time.deltaTime * speed;

                Vector3 slope1 = Vector3.Lerp(points[0].transform.position, points[2].transform.position, count);
                Vector3 slope2 = Vector3.Lerp(points[2].transform.position, points[1].transform.position, count);
                transform.position = Vector3.Lerp(slope1, slope2, count);
            }
            else if(count < 1 && goingBackwards)
            {
                count += Time.deltaTime * speed;

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

        //flip a platform
        if (flipPlatform)
        {
            if (shouldFlip)
            {
                transform.RotateAround(transform.position, Vector3.forward, speed);
                for (int i = -1; i <= 1; i+=2)
                {
                    if (transform.rotation == new Quaternion(0, 0, 0, i) || transform.rotation == new Quaternion(0, 0, i, 0))
                    {
                        shouldFlip = false;
                        break;
                    }
                }
            }
            else
            {
                flipCooldown += Time.deltaTime;
            }

            if (flipCooldown >= 3)
            {
                shouldFlip = true;
                flipCooldown = 0;
            }
        }

        //cooldown for returning speed of platform to normal
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
