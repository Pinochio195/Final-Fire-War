using UnityEngine;

public class State
{
    public FiniteStateMachine stateMachine;
    public BotController botController;

    public State(BotController botController, FiniteStateMachine stateMachine)
    {
        this.botController = botController;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
    public virtual void LogicUpdate()
    {
        
    }
    public virtual void PhysicsUpdate()
    {

    }
}