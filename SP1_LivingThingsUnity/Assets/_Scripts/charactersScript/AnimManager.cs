using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{

    [SerializeField] SpriteRenderer redNarmal;

    [SerializeField] SpriteRenderer redActiv;

    [SerializeField] SpriteRenderer blueNarmal;

    [SerializeField] SpriteRenderer blueActiv;

    bool Pull;
    //  bool Thrust;
    bool AbillityOn;
    // Use this for initialization
    void Start()
    {
        Pull = GetComponentInParent<Otter>().pullActiv;
        AbillityOn = GetComponentInParent<Otter>().magnetPowerActiv;
    }

    // Update is called once per frame
    void Update()
    {
        Pull = GetComponentInParent<Otter>().pullActiv;
        AbillityOn = GetComponentInParent<Otter>().magnetPowerActiv;
        if (Pull)
        {
            if (AbillityOn)
            {
                redActiv.enabled = true;
                redNarmal.enabled = false;
                blueActiv.enabled = false;
                blueNarmal.enabled = false;
            }
            else
            {
                redNarmal.enabled = true;
                redActiv.enabled = false;
                blueActiv.enabled = false;
                blueNarmal.enabled = false;
            }
        }
        else//Throst
        {
            if (AbillityOn)
            {
                blueActiv.enabled = true;
                redActiv.enabled = false;
                redNarmal.enabled = false;
                blueNarmal.enabled = false;

            }
            else
            {
                blueNarmal.enabled = true;
                redActiv.enabled = false;
                redNarmal.enabled = false;
                blueActiv.enabled = false;
            }
        }
    }
}
