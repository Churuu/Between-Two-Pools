using UnityEngine;

public class ChatState : ActivePlayerStateBase
{


    int backToCharakter = 1;
    [SerializeField] private string charakterOne = "1";
    [SerializeField] private string charakterTwo = "2";
    [SerializeField] private string charakterThree = "3";
    public ChatState(ActivePlayerStateMachine stateMachine)
    {
        if (stateMachines == null)
        {
            stateMachines = stateMachine;
        }
    }

    public override void UpdateState()
    {



    }
    public override void Enter()
    {

        EventManager.instance.OnChatEnd += ChangePlayer;



    }
    public override void Exit()
    {

        EventManager.instance.OnChatEnd -= ChangePlayer;



    }
    public override void OnTransision(ActivePlayerStateBase nextState)
    {
    }
    public override void OnTransisionFrom(ActivePlayerStateBase nextState)
    {
        if (nextState.numderCharakter != null)
        {
            backToCharakter = nextState.numderCharakter;
        }
    }
    void ChangePlayer()
    {

        if (backToCharakter > 3)
        {
            backToCharakter = 1;
        }


        if ( backToCharakter == 1)
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateOtter);

        }
        if ( backToCharakter == 2)
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateSeal);
        }
        if ( backToCharakter == 3)
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateFrog);
        }

    }
}
