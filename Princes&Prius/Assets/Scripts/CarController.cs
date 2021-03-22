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
    public AudioSource backgroundMusic;
    public GameObject beginning_1;
    public GameObject beginning_2;
    bool Beg_1 = false;
    bool Beg_2 = false;
    public GameObject enchantedForest_1;
    public GameObject enchantedForest_2;
    public GameObject goblinCamp_1;
    public GameObject goblinCamp_2;
    public GameObject wastelandDeserts_1;
    public GameObject wastelandDeserts_2;
    bool Enc_1 = false;
    bool Enc_2 = false;
    bool Gob_1 = false;
    bool Gob_2 = false;
    bool Was_1 = false;
    bool Was_2 = false;


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
    public IEnumerator DipVolume(GameObject gameObject)
    {
        AudioSource track = gameObject.GetComponent<AudioSource>();

        while (track.isPlaying)
        {
            Debug.Log("Is Playing");
            carNoise.volume = 0.2f;
            backgroundMusic.volume = 0.1f;
            yield return null;
        }
        carNoise.volume = 0.6f;
        backgroundMusic.volume = 0.4f;
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        string trackName = other.gameObject.name;

        switch(trackName)
        {
            case "Beginning_1":
                if (Beg_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    beginning_1.GetComponent<AudioSource>().Play();
                    Beg_1 = true;
                    StartCoroutine(DipVolume(beginning_1));
                }
                break;
            case "Beginning_2":
                if (Beg_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    beginning_2.GetComponent<AudioSource>().Play();
                    Beg_2 = true;
                    StartCoroutine(DipVolume(beginning_2));
                }
                break;
            case "EnchantedForest_1":
                if (Enc_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    enchantedForest_1.GetComponent<AudioSource>().Play();
                    Enc_1 = true;
                    StartCoroutine(DipVolume(enchantedForest_1));
                }
                break;
            case "EnchantedForest_2":
                if (Enc_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    enchantedForest_2.GetComponent<AudioSource>().Play();
                    Enc_2 = true;
                    StartCoroutine(DipVolume(enchantedForest_2));
                }
                break;
            case "GoblinCamp_1":
                if (Gob_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    goblinCamp_1.GetComponent<AudioSource>().Play();
                    Gob_1 = true;
                    StartCoroutine(DipVolume(goblinCamp_1));
                }
                break;
            case "GoblinCamp_2":
                if (Gob_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    goblinCamp_2.GetComponent<AudioSource>().Play();
                    Gob_2 = true;
                    StartCoroutine(DipVolume(goblinCamp_2));
                }
                break;
            case "WastelandDeserts_1":
                if (Was_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    wastelandDeserts_1.GetComponent<AudioSource>().Play();
                    Was_1 = true;
                    StartCoroutine(DipVolume(wastelandDeserts_1));
                }
                break;
            case "WastelandDeserts_2":
                if (Was_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    wastelandDeserts_2.GetComponent<AudioSource>().Play();
                    Was_2 = true;
                    StartCoroutine(DipVolume(wastelandDeserts_2));
                }
                break;
            default:
                break;
        }
    }
}