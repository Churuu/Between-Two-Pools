using UnityEngine;
public class ActivePlayerStateMachine : MonoBehaviour
{//Påbörjad av Jonas Thunberg 2019-02-04
    public GameObject Otter;
    public GameObject Seal;
    public GameObject Frog;
    [HideInInspector] private ActivePlayerStateBase curentState;

    //[HideInInspector] public StartState startState; //Start lägge//TODO
    [HideInInspector] public ActivePlayerOtterState activePlayerStateOtter;
    [HideInInspector] public ActivePlayerSealState activePlayerStateSeal;
    [HideInInspector] public ActivePlayerFrogState activePlayerStateFrog;
    [HideInInspector] public ChatState chatState; // Förlora Lägge//TODO
    //  [HideInInspector] public StartState startState; // Vinst Lägge//TODO
    // [HideInInspector] public StartState startState; // Paus lägge//TODO

    [SerializeField]
    private AudioClip switchToOtter;
    [SerializeField]
    private AudioClip switchToSeal;
    [SerializeField]
    private AudioClip switchToFrog;

    private void Awake()
    {
        // Start startState = new StartState(this);

        activePlayerStateOtter = new ActivePlayerOtterState (this, Otter,1);
    //    Debug.Log(activePlayerState1);
        activePlayerStateSeal = new ActivePlayerSealState(this, Seal,2);
     //   Debug.Log(activePlayerState2);
        activePlayerStateFrog = new ActivePlayerFrogState(this, Frog,3);
        //  Debug.Log(activePlayerState3);
        chatState = new ChatState(this);
    }
    private void Start()
    {
        ChangeState(activePlayerStateOtter);
    }
    void Update()
    {
        curentState.UpdateState();
    }
    public void ChangeState(ActivePlayerStateBase nextState)
    {
        if (curentState != null)
        {
          //  Debug.Log("not null");
            curentState.Exit();
            curentState.OnTransision(nextState);
        }

        
        nextState.Enter();
        nextState.OnTransisionFrom(curentState);
        curentState = nextState;
        PlaySwitchAudio(nextState); //Kallar på funktionen som sedan kör igång ljud för karaktärsbyte
    }
    //Funktion for karaktärsbytesljud
    private void PlaySwitchAudio(ActivePlayerStateBase nextState)
    {
        if (nextState != chatState) //om nextState inte är chat så kollar den igenom vilken state den är, 
            //och kör ljudet som den behöver
        {
            if (nextState == activePlayerStateOtter)
            {
                GetComponent<ObjectAudioClip>().PlaySingle(switchToOtter);
                FindObjectOfType<StemsManager>().ToOtter();
            }
            if (nextState == activePlayerStateSeal)
            {
                GetComponent<ObjectAudioClip>().PlaySingle(switchToSeal);
                FindObjectOfType<StemsManager>().ToSeal();
            }
            if (nextState == activePlayerStateFrog)
            {
                GetComponent<ObjectAudioClip>().PlaySingle(switchToFrog);
                FindObjectOfType<StemsManager>().ToFrog();
            }
        }

    }
}
