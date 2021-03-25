using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void FireCannonBalls();
    public static event FireCannonBalls fireCannonBalls;
    public GameObject cannonball;

    public delegate void ThrowRocks();
    public static event ThrowRocks throwRocks;
    public GameObject rocktrigger;

    public static void ThrowRockEvent()
    {
        throwRocks();
    }
    public static void RunCannonBallEvent()
    {
        fireCannonBalls();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
