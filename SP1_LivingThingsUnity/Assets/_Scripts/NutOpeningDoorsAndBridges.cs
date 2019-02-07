// Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutOpeningDoorsAndBridges : MonoBehaviour {

    public GameObject ThingToBeOpened;
    public float OpeningSpeed;
    public string WhatdirectiontoMove;
    private bool SkiftnyckelAbilityActive;
    private WrenchCharacteristics AbilityActive;
    private float OpeningMoveing;
    private bool SkiftnyckelOnMutter;
    // Use this for initialization
    void Start()
    {
        GameObject Skiftnyckel = GameObject.Find("Wrench");
        AbilityActive = Skiftnyckel.GetComponent<WrenchCharacteristics>();

        SkiftnyckelOnMutter = false;
        if (WhatdirectiontoMove == "Vertical") OpeningMoveing = ThingToBeOpened.transform.position.y;
        else if (WhatdirectiontoMove == "Horizontal") OpeningMoveing = ThingToBeOpened.transform.position.x;


    }

    // Update is called once per frame
    void Update()
    {

        SkiftnyckelAbilityActive = AbilityActive.AbiltyActive;
        if (SkiftnyckelAbilityActive == false)
        {
            //OpeningMoveing = 0;
            SkiftnyckelOnMutter = false;
        }
        else { }

        if (SkiftnyckelOnMutter == true && SkiftnyckelAbilityActive == true)
        {
            if (WhatdirectiontoMove == "Vertical")
            {
                MoveObjectVertical();
            }
            else if (WhatdirectiontoMove == "Horizontal")
            {
                MoveObjectHorizontal();
            }

        }

    }
    void MoveObjectVertical()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OpeningMoveing = OpeningMoveing + OpeningSpeed;
            ThingToBeOpened.transform.position = new Vector3(ThingToBeOpened.transform.position.x, OpeningMoveing, 0);
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            OpeningMoveing = OpeningMoveing - OpeningSpeed;
            ThingToBeOpened.transform.position = new Vector3(ThingToBeOpened.transform.position.x, OpeningMoveing, 0);
        }
    }

    void MoveObjectHorizontal()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OpeningMoveing = OpeningMoveing + OpeningSpeed;
            ThingToBeOpened.transform.position = new Vector3(OpeningMoveing, ThingToBeOpened.transform.position.y, 0);
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            OpeningMoveing = OpeningMoveing - OpeningSpeed;
            ThingToBeOpened.transform.position = new Vector3(OpeningMoveing, ThingToBeOpened.transform.position.y, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wrench") && SkiftnyckelAbilityActive == true)
        {
            SkiftnyckelOnMutter = true;
        }
    }
}

