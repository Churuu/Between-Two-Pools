using UnityEngine;

public class ActivePlayerOtterState : ActivePlayerStateBase
{
    public GameObject snubbe;
    public int playerNumber;

    Otter otter;
    [SerializeField] private string charakterOne = "1";
    [SerializeField] private string charakterTwo = "2";
    [SerializeField] private string charakterThree = "3";

    public ActivePlayerOtterState(ActivePlayerStateMachine stateMachine, GameObject gameObjectPlayer, int number)
    {
        snubbe = gameObjectPlayer;
        if (stateMachines == null)
        {
            stateMachines = stateMachine;
        }
        otter = snubbe.GetComponent<Otter>();
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
        otter.okToShangeMagnet = true;

        stateMachines.transform.position = snubbe.transform.position;

        stateMachines.transform.parent = snubbe.transform;
        snubbe.GetComponent<PlayerController>().SetPlayerState(true);
        EventManager.instance.OnChatActiv += ChangeToChat;
    }
    public override void Exit()
    {
        otter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        otter.okToShangeMagnet = false;
        stateMachines.transform.parent = null;
        snubbe.GetComponent<PlayerController>().SetPlayerState(false);
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

        //if (Input.GetButtonDown(charakterOne) && playerNumber != 1)
        //{
        //    stateMachines.ChangeState(stateMachines.activePlayerStateOtter);

        //}
        if (Input.GetButtonDown(charakterTwo)) //&& playerNumber != 2
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateSeal);
        }
        if (Input.GetButtonDown(charakterThree))//&& playerNumber != 3
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateFrog);
        }
    }
   void ChangeToChat()
    {
        stateMachines.ChangeState(stateMachines.chatState);
    }
}