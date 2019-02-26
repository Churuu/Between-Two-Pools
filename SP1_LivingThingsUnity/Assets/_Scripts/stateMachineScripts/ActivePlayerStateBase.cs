//using UnityEditor;
using UnityEngine;
public abstract class ActivePlayerStateBase
{//Påbörjad av Jonas Thunberg 2019-02-04
    protected ActivePlayerStateMachine stateMachines;
    public virtual void UpdateState() { }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void OnTransision(ActivePlayerStateBase nextState) { }
    public virtual void OnTransisionFrom(ActivePlayerStateBase nextState) { }

   
    
}
