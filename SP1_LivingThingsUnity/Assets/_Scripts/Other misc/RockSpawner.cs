using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    public GameObject rock;
    public float timerDelta;

    float timer;

    GameObject _rock;

    void Start()
    {
        timer = timerDelta;
    }

    void Update()
    {
        if (_rock == null)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
                SpawnNewRock();
        }
    }


    void SpawnNewRock()
    {
        _rock = Instantiate(rock, transform.position, transform.rotation) as GameObject;
        timer = timerDelta;
    }
}
