using UnityEngine;

public class ActivePlayerWrenchState : ActivePlayerStateBase
{

    public GameObject wrench;
    public int playerNumber;

    [SerializeField] private string charakterOne = "1";
    [SerializeField] private string charakterTwo = "2";
    [SerializeField] private string charakterThree = "3";
    public ActivePlayerWrenchState(ActivePlayerStateMachine stateMachine, GameObject gameObjectPlayer, int number)
    {
        wrench = gameObjectPlayer;
        if (stateMachines == null)
        {
            stateMachines = stateMachine;
        }

        playerNumber = number;

    }

    public override void UpdateState()
    {

        ChangePlayer();

    }
    public override void Enter()
    {

        stateMachines.transform.position = wrench.transform.position;

        stateMachines.transform.parent = wrench.transform;
        wrench.GetComponent<WrenchCharacteristics>().enabled = true;
        if (!wrench.GetComponent<WrenchCharacteristics>().AbiltyActive)
        {
            wrench.GetComponent<Movement>().enabled = true;
        }
        wrench.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;


    }
    public override void Exit()
    {
        if (!wrench.GetComponent<WrenchCharacteristics>().AbiltyActive)
        {
            wrench.GetComponent<Movement>().enabled = false;
        }
        else
        {
            wrench.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        wrench.GetComponent<WrenchCharacteristics>().enabled = false;
        stateMachines.transform.parent = null;




    }
    public override void OnTransision(ActivePlayerStateBase nextState)
    {
    }
    public override void OnTransisionFrom(ActivePlayerStateBase nextState)
    {
    }
    void ChangePlayer()
    {




        if (Input.GetButtonDown(charakterOne) && playerNumber != 1)
        {
            stateMachines.ChangeState(stateMachines.activePlayerState1);

        }
        if (Input.GetButtonDown(charakterTwo) && playerNumber != 2)
        {
            stateMachines.ChangeState(stateMachines.activePlayerState2);
        }
        if (Input.GetButtonDown(charakterThree) && playerNumber != 3)
        {
            stateMachines.ChangeState(stateMachines.activePlayerState3);
        }

    }
}
