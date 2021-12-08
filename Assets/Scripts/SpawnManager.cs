using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    PlayerController playerControllerScript;
    float startDelay = 2f;
    float repeatRate = 2f;


    Vector3 spawnPos= new Vector3(25,0,0);
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }


    void SpawnObstacle ()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
        
    }

}
