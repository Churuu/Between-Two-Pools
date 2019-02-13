using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerSpringState : ActivePlayerStateBase {

    public GameObject spring;
    public int playerNumber;

    [SerializeField] private string charakterOne = "1";
    [SerializeField] private string charakterTwo = "2";
    [SerializeField] private string charakterThree = "3";
    public ActivePlayerSpringState(ActivePlayerStateMachine stateMachine, GameObject gameObjectPlayer, int number)
    {
        spring = gameObjectPlayer;
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

        stateMachines.transform.position = spring.transform.position;
        stateMachines.transform.parent = spring.transform;
        spring.GetComponent<PlayerController>().enabled = true;
        
    }
    public override void Exit()
    {

        stateMachines.transform.parent = null;
        spring.GetComponent<PlayerController>().enabled = false;



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
