using Ring;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    [HeaderTextColor(0.2f, .7f, .8f, headerText = "Player Component")] public BotController _botController;

    private void Start()
    {
    }

    /*void Update()
    {
        if(_botController._botController._checkZonePlayer  == BotManager.CheckPlayer.InSide)
        {
            if ( Vector3.Distance(PlayerManager.Instance.transform.position, _botController.transform.position) > 1f)
            {
                _botController._botController._positionDestination = new Vector3(PlayerManager.Instance._playerController._rotatePlayer.transform.position.x, 0, PlayerManager.Instance._playerController._rotatePlayer.transform.position.z);
                _botController._botController._navMeshAgent.SetDestination(_botController._botController._positionDestination);
                _botController._botController._navMeshAgent.isStopped = false;
               Debug.Log(123);
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Settings.Tag_Player))
        {
            _botController._botController._checkZonePlayer = BotManager.CheckPlayer.InSide;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Settings.Tag_Player))
        {
            if (Vector3.Distance(PlayerManager.Instance.transform.position, _botController.transform.position) > Settings.RadiusAttackPlayer)
            {
                if (_botController._botController._navMeshAgent.enabled)
                {
                    _botController._botController._positionDestination = GameManager.Instance.GetPositionPlayer(_botController);
                    _botController._botController._navMeshAgent.SetDestination(_botController._botController._positionDestination);
                    _botController._botController._navMeshAgent.isStopped = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Settings.Tag_Player))
        {
            _botController._botController._checkZonePlayer = BotManager.CheckPlayer.OutSide;
            _botController.stateMachine.ChangeState(_botController.idleState);
            _botController._botController._positionDestination = Vector3.zero;
            
        }
    }
}