using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    [SerializeField]
    private GameObject startPos;
    [SerializeField]
    private GameObject endPos;

    public bool isMoving;
    public bool isMovingBackwards;
    private float fractionOfJourney;
    private float startTime;
    private float journeyLength;
    private float distTravelled;
    [SerializeField]
    private float speed;

    public void BeginMovement()
    {
        Debug.Log("beginmovement called");
        StartCoroutine("StartMovement");
    }

    public IEnumerator StartMovement()
    {
        if (!isMoving && !isMovingBackwards)
        {
            Debug.Log("Coroutine Started");
            yield return new WaitForSeconds(1f);
            startTime = Time.time;
            isMoving = true;
            journeyLength = Vector3.Distance(startPos.transform.position, endPos.transform.position);
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distCovered / journeyLength;

            gameObject.transform.position = Vector3.Lerp(startPos.transform.position, endPos.transform.position, fractionOfJourney);
            if(fractionOfJourney >= 0.985f)
            {
                isMoving = false;
                isMovingBackwards = true;
                startTime = Time.time;
            }
        }
        else if (isMovingBackwards)
        {
            float distCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distCovered / journeyLength;
            gameObject.transform.position = Vector3.Lerp(endPos.transform.position, startPos.transform.position, fractionOfJourney);
            if(fractionOfJourney >= 0.985f)
            {
                isMoving = true;
                isMovingBackwards = false;
                startTime = Time.time;
            }
        }
    }
}
