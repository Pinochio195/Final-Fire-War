using UnityEngine;

public class IdleState : State
{
    private float TimeDelay;

    public IdleState(BotController botController, FiniteStateMachine stateMachine) : base(botController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        botController._botController._animator.SetInteger(Settings.Animation, 0);
        TimeDelay = 0;
        botController._botController._navMeshAgent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (botController._botController._checkZonePlayer == Ring.BotManager.CheckPlayer.OutSide)
        {
            if (TimeDelay > Random.Range(0, 5))
            {
                //stateMachine.ChangeState(botController.runState);
            }
            else
            {
                TimeDelay += Time.deltaTime;
            }
            //random di ngẫu nhiên 
        }
        else
        {
            //đi thằng về phía của player
            //botController._botController._positionDestination = new Vector3(PlayerManager.Instance._playerController._rotatePlayer.transform.position.x, 0, PlayerManager.Instance._playerController._rotatePlayer.transform.position.z);
            botController._botController._positionDestination = GameManager.Instance.GetPositionPlayer(botController);
            stateMachine.ChangeState(botController.runState);
        }
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}