using UnityEngine;

public class CheckDeadZone : MonoBehaviour
{
    [SerializeField] private BotController botController;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Settings.Tag_CheckZombie))
        {
            // Kiểm tra xem botController đã có trong danh sách chưa
            if (!PlayerManager.Instance._playerController._listBot.Contains(botController))
            {
                // Nếu chưa có, thì thêm vào danh sách
                PlayerManager.Instance._playerController._listBot.Add(botController);
                Debug.Log(other.name);
                PlayerManager.Instance._playerController.isCheckTarget = true;
            }
                GameManager.Instance.RotatePlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Settings.Tag_CheckZombie))
        {
            if (PlayerManager.Instance._playerController._listBot.Count > 0 )
            {
                //trừ
                PlayerManager.Instance._playerController._listBot.Remove(botController);
            }
                PlayerManager.Instance._playerController.isCheckTarget = false;
            
        }
    }
}