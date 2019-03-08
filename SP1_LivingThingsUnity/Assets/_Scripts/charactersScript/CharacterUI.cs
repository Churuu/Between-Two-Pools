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
    [Space]
    public Image selectedCharacterImage;

    public enum states { deadAndActive, inactive, active, deadAndInactive }
    public states otterState, frogState, sealState;

    OtterKilld otterKilld;
    FrogKilld frogKilld;
    SealKilld sealKilld;

    void Start()
    {
        sealKilld = FindObjectOfType<SealKilld>();
        otterKilld = FindObjectOfType<OtterKilld>();
        frogKilld = FindObjectOfType<FrogKilld>();
        EventManager.instance.OnNewActiveCharacter += SetActiveCharacter;
        EventManager.instance.onKilld += UpdateDeathState;
    }

    void Update()
    {
        States();
    }

    void UpdateDeathState(GameObject obj)
    {
        if (obj == otterKilld.gameObject && otterState == states.inactive)
            otterState = states.deadAndInactive;
        else if (obj == otterKilld.gameObject && otterState == states.active)
            otterState = states.deadAndActive;

        if (obj == sealKilld.gameObject && sealState == states.inactive)
            sealState = states.deadAndInactive;
        else if (obj == sealKilld.gameObject && sealState == states.active)
            sealState = states.deadAndActive;

        if (obj == frogKilld.gameObject && frogState == states.inactive)
            frogState = states.deadAndInactive;
        else if (obj == frogKilld.gameObject && frogState == states.active)
            frogState = states.deadAndActive;
    }


    void States()
    {
        switch (otterState)
        {
            case states.deadAndActive:
                otter.sprite = otterDead;
                break;
            case states.inactive:
                otter.sprite = otterAsleep;
                break;
            case states.active:
                otter.sprite = otterActive;
                break;
            case states.deadAndInactive:
                otter.sprite = otterDeadAndAsleep;
                break;
        }
        switch (frogState)
        {
            case states.deadAndActive:
                frog.sprite = frogDead;
                break;
            case states.inactive:
                frog.sprite = frogAsleep;
                break;
            case states.active:
                frog.sprite = frogActive;
                break;
            case states.deadAndInactive:
                frog.sprite = frogDeadAndAsleep;
                break;
        }
        switch (sealState)
        {
            case states.deadAndActive:
                seal.sprite = sealDead;
                break;
            case states.inactive:
                seal.sprite = sealAsleep;
                break;
            case states.active:
                seal.sprite = sealActive;
                break;
            case states.deadAndInactive:
                seal.sprite = sealDeadAndAsleep;
                break;
        }
    }

    void SetActiveCharacter(int character)
    {
        switch (character)
        {
            case 1:
                if (otterKilld.notKilld)
                    otterState = states.active;
                else
                    otterState = states.deadAndActive;

                if (sealKilld.notKilld)
                    sealState = states.inactive;
                else
                    sealState = states.deadAndInactive;

                if (frogKilld.notKilld)
                    frogState = states.inactive;
                else
                    frogState = states.deadAndInactive;

                selectedCharacterImage.sprite = otterSelected;
                break;
            case 2:
                if (otterKilld.notKilld)
                    otterState = states.inactive;
                else
                    otterState = states.deadAndInactive;

                if (sealKilld.notKilld)
                    sealState = states.active;
                else
                    sealState = states.deadAndActive;

                if (frogKilld.notKilld)
                    frogState = states.inactive;
                else
                    frogState = states.deadAndInactive;
                selectedCharacterImage.sprite = sealSelected;
                break;
            case 3:
                if (otterKilld.notKilld)
                    otterState = states.inactive;
                else
                    otterState = states.deadAndInactive;

                if (sealKilld.notKilld)
                    sealState = states.inactive;
                else
                    sealState = states.deadAndInactive;

                if (frogKilld.notKilld)
                    frogState = states.active;
                else
                    frogState = states.deadAndActive;
                selectedCharacterImage.sprite = frogSelected;
                break;
        }
    }
}