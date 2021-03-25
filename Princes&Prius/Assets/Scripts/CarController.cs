using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarController: MonoBehaviour {


    public float acceleration;
    public float steering;
    private Rigidbody2D rb;
    static public AudioSource carNoise;
    bool playingNoise = false;
    bool canStopNoise = false;
    static bool keepFadingIn;
    static bool keepFadingOut;
    public Image companionObj;
    public AudioSource backgroundMusic;
    public GameObject beginning_1;
    public GameObject beginning_2;
    public GameObject enchantedForest_1;
    public GameObject enchantedForest_2;
    public GameObject goblinCamp_1;
    public GameObject goblinCamp_2;
    public GameObject wastelandDeserts_1;
    public GameObject wastelandDeserts_2;
    public GameObject pirateSeas_1;
    public GameObject pirateSeas_2;
    public GameObject pirateSeas_3;
    public GameObject hauntedForest_1;
    public GameObject hauntedForest_2;
    public GameObject volcano_1;
    public GameObject volcano_2;
    public GameObject dragonsLair_1;
    public GameObject dragonsLair_2;
    public GameObject dragonsLair_3;
    public GameObject vampireCastle_1;
    public GameObject vampireCastle_2;
    public GameObject vampireCastle_3;
    public GameObject wizardTower_1;
    public GameObject wizardTower_2;
    public GameObject wizardTower_3;
    public GameObject cave_1;
    public GameObject cave_2;
    public GameObject treasureMine_1;
    public GameObject treasureMine_2;
    public GameObject treasureMine_3;
    public GameObject dragonLine_1;
    public GameObject dragonLine_2;
    public GameObject GoblinLine_1;
    public GameObject VampireLine_1;
    public GameObject VampireLine_2;
    public GameObject WizardLine_1;
    public GameObject WizardLine_2;
    public GameObject PirateLine_1;
    public GameObject PirateLine_2;
    bool pirate_1 = false;
    bool pirate_2 = false;
    bool wizard_1 = false;
    bool wizard_2 = false;
    bool vamp_1 = false;
    bool vamp_2 = false;
    bool gobLine_1 = false;
    bool draLine_1 = false;
    bool draLine_2 = false;
    bool beg_1 = false;
    bool beg_2 = false;
    bool enc_1 = false;
    bool enc_2 = false;
    bool gob_1 = false;
    bool gob_2 = false;
    bool was_1 = false;
    bool was_2 = false;
    bool pir_1 = false;
    bool pir_2 = false;
    bool pir_3 = false;
    bool hau_1 = false;
    bool hau_2 = false;
    bool vol_1 = false;
    bool vol_2 = false;
    bool dra_1 = false;
    bool dra_2 = false;
    bool dra_3 = false;
    bool vam_1 = false;
    bool vam_2 = false;
    bool vam_3 = false;
    bool wiz_1 = false;
    bool wiz_2 = false;
    bool wiz_3 = false;
    bool cav_1 = false;
    bool cav_2 = false;
    bool tre_1 = false;
    bool tre_2 = false;
    bool tre_3 = false;
    bool can_1 = false;


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
        companionObj.GetComponent<CompanionSprite>().FadeOut();
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        string trackName = other.gameObject.name;

        switch(trackName)
        {
            case "Beginning_1":
                if (beg_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    beginning_1.GetComponent<AudioSource>().Play();
                    beg_1 = true;
                    StartCoroutine(DipVolume(beginning_1));
                }
                break;
            case "Beginning_2":
                if (beg_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    beginning_2.GetComponent<AudioSource>().Play();
                    beg_2 = true;
                    StartCoroutine(DipVolume(beginning_2));
                }
                break;
            case "EnchantedForest_1":
                if (enc_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    enchantedForest_1.GetComponent<AudioSource>().Play();
                    enc_1 = true;
                    StartCoroutine(DipVolume(enchantedForest_1));
                }
                break;
            case "EnchantedForest_2":
                if (enc_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    enchantedForest_2.GetComponent<AudioSource>().Play();
                    enc_2 = true;
                    StartCoroutine(DipVolume(enchantedForest_2));
                }
                break;
            case "GoblinCamp_1":
                if (gob_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    goblinCamp_1.GetComponent<AudioSource>().Play();
                    gob_1 = true;
                    StartCoroutine(DipVolume(goblinCamp_1));
                }
                break;
            case "GoblinCamp_2":
                if (gob_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    goblinCamp_2.GetComponent<AudioSource>().Play();
                    gob_2 = true;
                    StartCoroutine(DipVolume(goblinCamp_2));
                }
                break;
            case "WastelandDeserts_1":
                if (was_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    wastelandDeserts_1.GetComponent<AudioSource>().Play();
                    was_1 = true;
                    StartCoroutine(DipVolume(wastelandDeserts_1));
                }
                break;
            case "WastelandDeserts_2":
                if (was_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    wastelandDeserts_2.GetComponent<AudioSource>().Play();
                    was_2 = true;
                    StartCoroutine(DipVolume(wastelandDeserts_2));
                }
                break;
            case "PirateSeas_1":
                if (pir_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    pirateSeas_1.GetComponent<AudioSource>().Play();
                    pir_1 = true;
                    StartCoroutine(DipVolume(pirateSeas_1));
                }
                break;
            case "PirateSeas_2":
                if (pir_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    pirateSeas_2.GetComponent<AudioSource>().Play();
                    pir_2 = true;
                    StartCoroutine(DipVolume(pirateSeas_2));
                }
                break;
            case "PirateSeas_3":
                if (pir_3 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    pirateSeas_3.GetComponent<AudioSource>().Play();
                    pir_3 = true;
                    StartCoroutine(DipVolume(pirateSeas_3));
                }
                break;
            case "HauntedForest_1":
                if (hau_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    hauntedForest_1.GetComponent<AudioSource>().Play();
                    hau_1 = true;
                    StartCoroutine(DipVolume(hauntedForest_1));
                }
                break;
            case "HauntedForest_2":
                if (hau_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    hauntedForest_2.GetComponent<AudioSource>().Play();
                    hau_2 = true;
                    StartCoroutine(DipVolume(hauntedForest_2));
                }
                break;
            case "Volcano_1":
                if (vol_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    volcano_1.GetComponent<AudioSource>().Play();
                    vol_1 = true;
                    StartCoroutine(DipVolume(volcano_1));
                }
                break;
            case "Volcano_2":
                if (vol_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    volcano_2.GetComponent<AudioSource>().Play();
                    vol_2 = true;
                    StartCoroutine(DipVolume(volcano_2));
                }
                break;
            case "DragonsLair_1":
                if (dra_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    dragonsLair_1.GetComponent<AudioSource>().Play();
                    dra_1 = true;
                    StartCoroutine(DipVolume(dragonsLair_1));
                }
                break;
            case "DragonsLair_2":
                if (dra_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    dragonsLair_2.GetComponent<AudioSource>().Play();
                    dra_2 = true;
                    StartCoroutine(DipVolume(dragonsLair_2));
                }
                break;
            case "DragonsLair_3":
                if (dra_3 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    dragonsLair_3.GetComponent<AudioSource>().Play();
                    dra_3 = true;
                    StartCoroutine(DipVolume(dragonsLair_3));
                }
                break;
            case "VampireCastle_1":
                if (vam_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    vampireCastle_1.GetComponent<AudioSource>().Play();
                    vam_1 = true;
                    StartCoroutine(DipVolume(vampireCastle_1));
                }
                break;
            case "VampireCastle_2":
                if (vam_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    vampireCastle_2.GetComponent<AudioSource>().Play();
                    vam_2 = true;
                    StartCoroutine(DipVolume(vampireCastle_2));
                }
                break;
            case "VampireCastle_3":
                if (vam_3 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    vampireCastle_3.GetComponent<AudioSource>().Play();
                    vam_3 = true;
                    StartCoroutine(DipVolume(vampireCastle_3));
                }
                break;
            case "WizardTower_1":
                if (wiz_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    wizardTower_1.GetComponent<AudioSource>().Play();
                    wiz_1 = true;
                    StartCoroutine(DipVolume(wizardTower_1));
                }
                break;
            case "WizardTower_2":
                if (wiz_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    wizardTower_2.GetComponent<AudioSource>().Play();
                    wiz_2 = true;
                    StartCoroutine(DipVolume(wizardTower_2));
                }
                break;
            case "WizardTower_3":
                if (wiz_3 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    wizardTower_3.GetComponent<AudioSource>().Play();
                    wiz_3 = true;
                    StartCoroutine(DipVolume(wizardTower_3));
                }
                break;
            case "Cave_1":
                if (cav_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    cave_1.GetComponent<AudioSource>().Play();
                    cav_1 = true;
                    StartCoroutine(DipVolume(cave_1));
                }
                break;
            case "Cave_2":
                if (cav_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    cave_2.GetComponent<AudioSource>().Play();
                    cav_2 = true;
                    StartCoroutine(DipVolume(cave_2));
                }
                break;
            case "TreasureMine_1":
                if (tre_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    treasureMine_1.GetComponent<AudioSource>().Play();
                    tre_1 = true;
                    StartCoroutine(DipVolume(treasureMine_1));
                }
                break;
            case "TreasureMine_2":
                if (tre_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    treasureMine_2.GetComponent<AudioSource>().Play();
                    tre_2 = true;
                    StartCoroutine(DipVolume(treasureMine_2));
                }
                break;
            case "TreasureMine_3":
                if (tre_3 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    treasureMine_3.GetComponent<AudioSource>().Play();
                    tre_3 = true;
                    StartCoroutine(DipVolume(treasureMine_3));
                }
                break;
            case "DragonLine_1":
                if (draLine_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    dragonLine_1.GetComponent<AudioSource>().Play();
                    draLine_1 = true;
                    StartCoroutine(DipVolume(dragonLine_1));
                }
                break;
            case "DragonLine_2":
                if (draLine_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    dragonLine_2.GetComponent<AudioSource>().Play();
                    draLine_2 = true;
                    StartCoroutine(DipVolume(dragonLine_2));
                }
                break;
            case "GoblinLine_1":
                if (gobLine_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    GoblinLine_1.GetComponent<AudioSource>().Play();
                    gobLine_1 = true;
                    StartCoroutine(DipVolume(GoblinLine_1));
                }
                break;
            case "VampireLine_1":
                if (vamp_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    VampireLine_1.GetComponent<AudioSource>().Play();
                    vamp_1 = true;
                    StartCoroutine(DipVolume(VampireLine_1));
                }
                break;
            case "VampireLine_2":
                if (vamp_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    VampireLine_2.GetComponent<AudioSource>().Play();
                    vamp_2 = true;
                    StartCoroutine(DipVolume(VampireLine_2));
                }
                break;
            case "WizardLine_1":
                if (wizard_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    WizardLine_1.GetComponent<AudioSource>().Play();
                    wizard_1 = true;
                    StartCoroutine(DipVolume(WizardLine_1));
                }
                break;
            case "WizardLine_2":
                if (wizard_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    WizardLine_2.GetComponent<AudioSource>().Play();
                    wizard_2 = true;
                    StartCoroutine(DipVolume(WizardLine_2));
                }
                break;
            case "PirateLine_1":
                if (pirate_1 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    PirateLine_1.GetComponent<AudioSource>().Play();
                    pirate_1 = true;
                    StartCoroutine(DipVolume(PirateLine_1));
                }
                break;
            case "PirateLine_2":
                if (pirate_2 == false)
                {
                    Debug.Log("Entered Trigger");
                    companionObj.GetComponent<CompanionSprite>().FadeIn();
                    PirateLine_2.GetComponent<AudioSource>().Play();
                    pirate_2 = true;
                    StartCoroutine(DipVolume(PirateLine_2));
                }
                break;
            case "CannonBall":
                if (can_1 == false)
                {
                    EventManager.RunCannonBallEvent();
                    can_1 = true;
                }
                break;
            default:
                break;
        }
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}