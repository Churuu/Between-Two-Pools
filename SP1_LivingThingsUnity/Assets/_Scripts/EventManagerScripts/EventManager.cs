using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    //Jonas Thunberg 2019-02-26
    public static EventManager instance;
    //public delegate void events(GameObject n);
    //public static events onKilled;

    public System.Action<GameObject> onKilld;
    public System.Action OnGameOver;
    public System.Action<Rigidbody2D> OnStartAddMagneticWall;// Only Otter
    public System.Action<Rigidbody2D> OnStartAddMagneticBox;// Only Otter
    public System.Action OnChatActiv; //aktiverar chatState
    public System.Action OnChatEnd; // Bytt tillbacka till karaktär
    public System.Action<int> OnNewActiveCharacter; // För att veta vilken karaktär som är aktiv ! Otter, 2 Seal, 3 Frog.


    private void Awake()
    {
        instance = this;

    }
}
