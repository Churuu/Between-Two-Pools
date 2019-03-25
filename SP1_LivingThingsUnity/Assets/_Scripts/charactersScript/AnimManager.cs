using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{

    [SerializeField] SpriteRenderer redNarmal;
    [SerializeField] AudioSource redNormalAudio;

    [SerializeField] SpriteRenderer redActiv;
    [SerializeField] AudioSource redActiveAudio;

    [SerializeField] SpriteRenderer blueNarmal;
    [SerializeField] AudioSource blueNormalAudio;

    [SerializeField] SpriteRenderer blueActiv;
    [SerializeField] AudioSource blueActiveAudio;

    public GameObject activeObject;

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
                redActiveAudio.enabled = true;
                redNarmal.enabled = false;
                redNormalAudio.enabled = false;
                blueActiv.enabled = false;
                blueActiveAudio.enabled = false;
                blueNarmal.enabled = false;
                blueNormalAudio.enabled = false;

                activeObject = redActiveAudio.gameObject;
            }
            else
            {
                redNarmal.enabled = true;
                redNormalAudio.enabled = true;
                redActiv.enabled = false;
                redActiveAudio.enabled = false;
                blueActiv.enabled = false;
                blueActiveAudio.enabled = false;
                blueNarmal.enabled = false;
                blueNormalAudio.enabled = false;

                activeObject = redNormalAudio.gameObject;
            }
        }
        else//Throst
        {
            if (AbillityOn)
            {
                blueActiv.enabled = true;
                blueActiveAudio.enabled = true;
                redActiv.enabled = false;
                redActiveAudio.enabled = false;
                redNarmal.enabled = false;
                redNormalAudio.enabled = false;
                blueNarmal.enabled = false;
                blueNormalAudio.enabled = false;

                activeObject = blueActiveAudio.gameObject;
            }
            else
            {
                blueNarmal.enabled = true;
                blueNormalAudio.enabled = true;
                redActiv.enabled = false;
                redActiveAudio.enabled = false;
                redNarmal.enabled = false;
                redNormalAudio.enabled = false;
                blueActiv.enabled = false;
                blueActiveAudio.enabled = false;

                activeObject = blueNormalAudio.gameObject;
            }
        }
    }
}
