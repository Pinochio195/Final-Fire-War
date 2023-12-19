using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(BotController botController, FiniteStateMachine stateMachine) : base(botController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //botController._botController._animator.SetInteger(Settings.Animation, 2);
        botController._botController._animator.SetBool(Settings.AnimationCheckAttack_Bl, true);
        botController._botController._animator.SetFloat(Settings.AnimationAttack_Bl, Random.Range(0,2));
        botController._botController._animator.applyRootMotion = true;
        botController._botController._navMeshAgent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
        botController._botController._animator.applyRootMotion = false;
        botController._botController._rotateZombie.localPosition = Vector3.zero;
        //botController._botController._positionDestination = Vector3.zero;
        

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Mathf.Abs(Vector3.Distance(PlayerManager.Instance.transform.position, botController.transform.position) - Settings.RadiusAttackPlayer) >= .5f)
        {
            botController._botController._animator.SetBool(Settings.AnimationCheckAttack_Bl, false);
            stateMachine.ChangeState(botController.idleState);//chuyển trạng thái sang attack
        }
        else
        {
            //GameManager.Instance.RotateZombie(botController);
        }
    }
   
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}