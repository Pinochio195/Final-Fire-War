using UnityEngine;

public class RunState : State
{
    public RunState(BotController botController, FiniteStateMachine stateMachine) : base(botController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        botController._botController._animator.SetInteger(Settings.Animation, 1);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs((float)Vector3.Distance(PlayerManager.Instance.transform.position, botController.transform.position) - Settings.RadiusAttackPlayer) <= .01f)
        {
            stateMachine.ChangeState(botController.attackState);//chuyển trạng thái sang attack
        }
        GameManager.Instance.RotateZombie(botController);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}