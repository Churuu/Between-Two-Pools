using UnityEngine;
public class ActivePlayerState : ActivePlayerStateBase
{
    public GameObject snubbe;
    public int playerNumber;
    bool changePlayer = false;
    [SerializeField] private string charakterOne = "1";
    [SerializeField] private string charakterTwo = "2";
    [SerializeField] private string charakterThree = "3";
    public ActivePlayerState(ActivePlayerStateMachine stateMachine, GameObject gameObjectPlayer, int number)
    {
        snubbe = gameObjectPlayer;
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
        changePlayer = false;
        stateMachines.transform.position = snubbe.transform.position;
        //   snubbe.transform.parent = stateMachines.transform; 
        stateMachines.transform.parent = snubbe.transform;
        snubbe.GetComponent<Movement>().enabled = true;
    }
    public override void Exit()
    {//child.transform.parent = null;
     //    snubbe.transform.parent = null;
        stateMachines.transform.parent = null;
        snubbe.GetComponent<Movement>().enabled = false;



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
