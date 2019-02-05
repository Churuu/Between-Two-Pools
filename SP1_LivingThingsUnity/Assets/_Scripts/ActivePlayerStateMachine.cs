using UnityEngine;
public class ActivePlayerStateMachine : MonoBehaviour
{//Påbörjad av Jonas Thunberg 2019-02-04
    public GameObject spelareOne;
    public GameObject spelareTwo;
    public GameObject spelareThree;
    [HideInInspector] private ActivePlayerStateBase curentState;

    //[HideInInspector] public StartState startState; //Start lägge//TODO
    [HideInInspector] public ActivePlayerMagnetState activePlayerState1;
    [HideInInspector] public ActivePlayerState activePlayerState2;
    [HideInInspector] public ActivePlayerState activePlayerState3;
    // [HideInInspector] public GameOver startState; // Förlora Lägge//TODO
    //  [HideInInspector] public StartState startState; // Vinst Lägge//TODO
    // [HideInInspector] public StartState startState; // Paus lägge//TODO
    private void Awake()
    {
        // Start startState = new StartState(this);

        activePlayerState1 = new ActivePlayerMagnetState (this, spelareOne,1);
    //    Debug.Log(activePlayerState1);
        activePlayerState2 = new ActivePlayerState(this, spelareTwo,2);
     //   Debug.Log(activePlayerState2);
        activePlayerState3 = new ActivePlayerState(this, spelareThree,3);
      //  Debug.Log(activePlayerState3);
    }
    private void Start()
    {
       
        
        ChangeState(activePlayerState1);
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
    }






}
