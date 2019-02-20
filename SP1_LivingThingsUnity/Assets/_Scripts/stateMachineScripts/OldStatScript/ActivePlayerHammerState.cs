using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerHammerState : ActivePlayerStateBase {

    public GameObject hammer;
    public int playerNumber;

    [SerializeField] private string charakterOne = "1";
    [SerializeField] private string charakterTwo = "2";
    [SerializeField] private string charakterThree = "3";
    public ActivePlayerHammerState(ActivePlayerStateMachine stateMachine, GameObject gameObjectPlayer, int number)
    {
        hammer = gameObjectPlayer;
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

        stateMachines.transform.position = hammer.transform.position;
        hammer.GetComponent<Hammer>().ChangeActiveState();
        stateMachines.transform.parent = hammer.transform;
        hammer.GetComponent<PlayerController>().enabled = true;
    }
    public override void Exit()
    {
        hammer.GetComponent<Hammer>().ChangeActiveState();
        stateMachines.transform.parent = null;
        hammer.GetComponent<PlayerController>().enabled = false;



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
            stateMachines.ChangeState(stateMachines.activePlayerStateOtter);

        }
        if (Input.GetButtonDown(charakterTwo) && playerNumber != 2)
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateSeal);
        }
        if (Input.GetButtonDown(charakterThree) && playerNumber != 3)
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateFrog);
        }

    }
}
