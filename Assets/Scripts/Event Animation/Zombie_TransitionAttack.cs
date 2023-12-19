using UnityEngine;

public class Zombie_TransitionAttack : MonoBehaviour
{
    [SerializeField] private BotController botController;


    private void Update()
    {
        
    }

    public void TransitionAttack()
    {
        botController._botController._animator.SetFloat(Settings.AnimationAttack_Bl, Random.Range(0, 2));
    }

   

    public void TransitionRun()
    {
        botController._botController._navMeshAgent.isStopped = true;
    }
}