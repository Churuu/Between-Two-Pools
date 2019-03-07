using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{


    [Header("Otter Sprites")]
    public Image otter;
    public Sprite otterDead, otterAsleep, otterDeadAndAsleep, otterSelected, otterActive;
    [Space]
    [Header("Seal Sprites")]
    public Image seal;
    public Sprite sealDead, sealAsleep, sealDeadAndAsleep, sealSelected, sealActive;
    [Space]
    [Header("Frog Sprites")]
    public Image frog;
    public Sprite frogDead, frogAsleep, frogDeadAndAsleep, frogSelected, frogActive;


    public enum states {dead, asleep, active, deadAndAsleep }
    public states otterState, frogState, sealState;

    void Start()
    {

    }

    void Update()
    {
        States();
    }


    void States()
    {
        switch (otterState)
        {
            case states.dead:
                otter.sprite = otterDead;
                break;
            case states.asleep:
                otter.sprite = otterAsleep;
                break;
            case states.active:
                otter.sprite = otterActive;
                break;
            case states.deadAndAsleep:
                otter.sprite = otterDeadAndAsleep;
                break;
        }
        switch (frogState)
        {
            case states.dead:
                frog.sprite = frogDead;
                break;
            case states.asleep:
                frog.sprite = frogAsleep;
                break;
            case states.active:
                frog.sprite = frogActive;
                break;
            case states.deadAndAsleep:
                frog.sprite = frogDeadAndAsleep;
                break;
        }
        switch (sealState)
        {
            case states.dead:
                seal.sprite = sealDead;
                break;
            case states.asleep:
                seal.sprite = sealAsleep;
                break;
            case states.active:
                seal.sprite = sealActive;
                break;
            case states.deadAndAsleep:
                seal.sprite = sealDeadAndAsleep;
                break;
        }
    }
}