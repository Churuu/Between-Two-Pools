using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    public GameObject rock;

    GameObject _rock;

    void Start()
    {

    }

    void Update()
    {
        if (_rock != null)
            SpawnNewRock();
    }


    void SpawnNewRock()
    {
        _rock = Instantiate(rock, transform.position, transform.rotation) as GameObject;
    }
}
