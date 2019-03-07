using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerSealState : ActivePlayerStateBase {

    public GameObject seal;
    public int playerNumber;

    [SerializeField] private string charakterOne = "1";
    [SerializeField] private string charakterTwo = "2";
    [SerializeField] private string charakterThree = "3";
    public ActivePlayerSealState(ActivePlayerStateMachine stateMachine, GameObject gameObjectPlayer, int number)
    {
        seal = gameObjectPlayer;
        if (stateMachines == null)
        {
            stateMachines = stateMachine;
        }

        playerNumber = number;
        numderCharakter = playerNumber;
    }

    public override void UpdateState()
    {

        ChangePlayer();

    }
    public override void Enter()
    {
        if (EventManager.instance.OnNewActiveCharacter != null)
        {
            EventManager.instance.OnNewActiveCharacter(this.numderCharakter);
        }

        stateMachines.transform.position = seal.transform.position;
        stateMachines.transform.parent = seal.transform;
        seal.GetComponent<PlayerController>().SetPlayerState(true);
        EventManager.instance.OnChatActiv += ChangeToChat;
    }
    public override void Exit()
    {
        seal.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        stateMachines.transform.parent = null;
        seal.GetComponent<PlayerController>().SetPlayerState(false);
        EventManager.instance.OnChatActiv -= ChangeToChat;
    }
    public override void OnTransision(ActivePlayerStateBase nextState)
    {
    }
    public override void OnTransisionFrom(ActivePlayerStateBase nextState)
    {
    }
    void ChangePlayer()
    {




        if (Input.GetButtonDown(charakterOne) )//&& playerNumber != 1
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateOtter);

        }
        //if (Input.GetButtonDown(charakterTwo) && playerNumber != 2)
        //{
        //    stateMachines.ChangeState(stateMachines.activePlayerStateSeal);
        //}
        if (Input.GetButtonDown(charakterThree) )//&& playerNumber != 3
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateFrog);
        }

    }
    void ChangeToChat()
    {
        stateMachines.ChangeState(stateMachines.chatState);
    }
}
