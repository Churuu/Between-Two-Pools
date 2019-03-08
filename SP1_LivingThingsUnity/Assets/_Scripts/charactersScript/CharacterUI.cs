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

    public enum states { dead, asleep, active, deadAndInactive }
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
        if (obj == otterKilld.gameObject)
            otterState = states.dead;
        if (obj == sealKilld.gameObject)
            sealState = states.dead;
        if (obj == frogKilld.gameObject)
            frogState = states.dead;
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
            case states.deadAndInactive:
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
            case states.deadAndInactive:
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
                    otterState = states.dead;

                if (sealKilld.notKilld)
                    sealState = states.asleep;
                else
                    sealState = states.deadAndInactive;

                if (frogKilld.notKilld)
                    frogState = states.asleep;
                else
                    frogState = states.deadAndInactive;

                    selectedCharacterImage.sprite = otterSelected;
                break;
            case 2:
                if (otterKilld.notKilld)
                    otterState = states.asleep;
                else
                    otterState = states.deadAndInactive;

                if (sealKilld.notKilld)
                    sealState = states.active;
                else
                    sealState = states.dead;

                if (frogKilld.notKilld)
                    frogState = states.asleep;
                else
                    frogState = states.deadAndInactive;
                selectedCharacterImage.sprite = sealSelected;
                break;
            case 3:
                if (otterKilld.notKilld)
                    otterState = states.asleep;
                else
                    otterState = states.deadAndInactive;

                if (sealKilld.notKilld)
                    sealState = states.asleep;
                else
                    sealState = states.deadAndInactive;

                if (frogKilld.notKilld)
                    frogState = states.active;
                else
                    frogState = states.dead;
                selectedCharacterImage.sprite = frogSelected;
                break;
        }
    }
}