using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    bool moving = false;
    float elapsedTime;
    public void SetActive()
    {
        Debug.Log("set active");
        moving = true;
        elapsedTime = Time.time;
    }
    private void OnEnable()
    {
        Debug.Log("on enable");
        EventManager.throwRocks += SetActive;
    }
    private void OnDisable()
    {
        EventManager.throwRocks -= SetActive;
    }
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units per second.
    private float speed = 5.0f;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Move to the target end position.
    void Update()
    {
        if (moving == true)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime - elapsedTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
        }
    }
}
