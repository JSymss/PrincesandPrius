using UnityEngine;
using System.Collections;

public class CarController: MonoBehaviour {


    public float acceleration;
    public float steering;
    private Rigidbody2D rb;
    static public AudioSource carNoise;
    bool playingNoise = false;
    bool canStopNoise = false;
    static bool keepFadingIn;
    static bool keepFadingOut;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    carNoise = GetComponent<AudioSource>();
}

    public static IEnumerator FadeIn()
    {
        Debug.Log("Fading In");
        carNoise.Play();
        carNoise.volume = 0;
        float speed = 0.005f;

        for (float i = 0; i <= 0.6f; i += speed)
        {
            carNoise.volume = i;
            yield return null;
        }
    }
    public static IEnumerator FadeOut()
    {
        Debug.Log("Fading Out");
        carNoise.volume = 0.6f;
        float speed = 0.005f;

        for (float i = 0.6f; i >= 0; i -= speed)
        {
            carNoise.volume = i;
            yield return null;
        }
        
    }
    void FixedUpdate()
{
    float h = -Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    //Debug.Log(rb.velocity.magnitude);

        if (playingNoise == false && rb.velocity.magnitude >= 2)
        {
            Debug.Log("Reached speed");
            StartCoroutine(CarController.FadeIn());
            playingNoise = true;
            canStopNoise = true;
        }

        if (canStopNoise == true && rb.velocity.magnitude <=2)
        {
            StartCoroutine(CarController.FadeOut());
            playingNoise = false;
            canStopNoise = false;
        }
     

    Vector2 speed = transform.up * (v * acceleration);
    rb.AddForce(speed);

    float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
    if (direction >= 0.0f)
    {
        rb.rotation += h * steering * (rb.velocity.magnitude / 5.0f);
        //rb.AddTorque((h * steering) * (rb.velocity.magnitude / 10.0f));
    }
    else
    {
        rb.rotation -= h * steering * (rb.velocity.magnitude / 5.0f);
        //rb.AddTorque((-h * steering) * (rb.velocity.magnitude / 10.0f));
    }

    Vector2 forward = new Vector2(0.0f, 0.5f);
    float steeringRightAngle;
    if (rb.angularVelocity > 0)
    {
        steeringRightAngle = -90;
    }
    else
    {
        steeringRightAngle = 90;
    }

    Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
    Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(rightAngleFromForward), Color.green);

    float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

    Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);


    Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(relativeForce), Color.red);

    rb.AddForce(rb.GetRelativeVector(relativeForce));
}
 }