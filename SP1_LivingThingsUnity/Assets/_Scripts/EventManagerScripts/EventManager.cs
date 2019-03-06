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



    private void Awake()
    {
        instance = this;

    }
}
