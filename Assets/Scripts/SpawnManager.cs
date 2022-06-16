using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab; 
    PlayerController playerControllerScript;
    float startDelay = 1f;
    float repeatRate = 2.3f;

    float levelUp = 8f;

    Vector3 spawnPos= new Vector3(30,0,0);

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        Invoker();
       
    }


    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);            
        }
    }

    void Invoker()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        StartCoroutine(waitFor(levelUp));
    }

    IEnumerator waitFor(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        CancelInvoke();
        if(repeatRate != 1.3)
        {
           repeatRate = repeatRate - 0.3f;
       
        }
        if(repeatRate <= 1.3)
        {
            CancelInvoke();
            yield return new WaitForSeconds(3f); 
        
            Time.timeScale = 0;
        }
      
        Invoker();
        Debug.Log(repeatRate);
    }
}
