using UnityEngine;

public class ActivePlayerFrogState : ActivePlayerStateBase
{

    public GameObject frog;
    public int playerNumber;

    [SerializeField] private string charakterOne = "1";
    [SerializeField] private string charakterTwo = "2";
    [SerializeField] private string charakterThree = "3";
    public ActivePlayerFrogState(ActivePlayerStateMachine stateMachine, GameObject gameObjectPlayer, int number)
    {
        frog = gameObjectPlayer;
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

        stateMachines.transform.position = frog.transform.position;

        stateMachines.transform.parent = frog.transform;
        frog.GetComponent<Frog>().SwitchActivation(true);
        frog.GetComponent<PlayerController>().enabled = true;
    }
    public override void Exit()
    {
        frog.GetComponent<Frog>().SwitchActivation(false);
        frog.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        stateMachines.transform.parent = null;
        frog.GetComponent<PlayerController>().enabled = false;



    }
    public override void OnTransision(ActivePlayerStateBase nextState)
    {
    }
    public override void OnTransisionFrom(ActivePlayerStateBase nextState)
    {
    }
    void ChangePlayer()
    {




        if (Input.GetButtonDown(charakterOne))//&& playerNumber != 1
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateOtter);

        }
        if (Input.GetButtonDown(charakterTwo))//&& playerNumber != 2
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateSeal);
        }
        //if (Input.GetButtonDown(charakterThree) && playerNumber != 3)
        //{
        //    stateMachines.ChangeState(stateMachines.activePlayerStateFrog);
        //}

    }
}
