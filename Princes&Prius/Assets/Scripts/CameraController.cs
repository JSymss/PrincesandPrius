using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    int DistanceAway = 10;
    public float yDist = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        Vector3 PlayerPOS = GameObject.Find("Prius").transform.transform.position;
        GameObject.Find("MainCamera").transform.position = new Vector3(PlayerPOS.x, PlayerPOS.y + yDist, PlayerPOS.z - DistanceAway);
    }
}
