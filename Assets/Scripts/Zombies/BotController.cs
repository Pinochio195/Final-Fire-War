using Ring;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [HeaderTextColor(0.2f, .7f, .8f, headerText = "Player Component")] public BotManager _botController;
    public IdleState idleState;
    public RunState runState;
    public DieState dieState;
    public AttackState attackState;
    public FiniteStateMachine stateMachine;

    private void Start()
    {
        Intil();
        StateMachineBot();
    }

    private void StateMachineBot()
    {
        stateMachine = new FiniteStateMachine();
        runState = new RunState(this, stateMachine);
        idleState = new IdleState(this, stateMachine);
        dieState = new DieState(this, stateMachine);
        attackState = new AttackState(this, stateMachine);
        stateMachine.Initialize(idleState);
    }

    private void Intil()
    {
    }

    public void ActiveRagdoll()
    {
        _botController._animator.enabled = false;
        _botController._listRigidbody.ForEach(a => a.isKinematic = false);
        _botController._navMeshAgent.isStopped = true;
        gameObject.layer = LayerMask.NameToLayer(Settings.LayerDie);
        _botController._navMeshAgent.enabled = false;
        _botController._touchBullet._imageDamage.gameObject.SetActive(false);
        gameObject.tag = Settings.Tag_Die;
        PlayerManager.Instance._playerController._listBot.Remove(this);
        _botController._positionDestination = Vector3.zero;
        this.enabled = false;
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
}