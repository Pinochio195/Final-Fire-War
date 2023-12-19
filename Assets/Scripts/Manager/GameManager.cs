using Ring;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : RingSingleton<GameManager>
{
    [HeaderTextColor(0.2f, .7f, .8f, headerText = "CheckBox For Player")] public GameController _gameController;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        //UiManager.Instance.OpenUI<GamePlay>();//gọi UI đầu tiên
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GetPositionPlayer(BotController botController)
    {
        Vector3 positionA = PlayerManager.Instance.transform.position;
        Vector3 direction = botController.transform.position - positionA;
        //float distance = direction.magnitude;
        // Kiểm tra xem objectB có ở ngoài bán kính không
        /*if (distance > radius)
        {
        }*/
        Vector3 targetPosition = positionA + direction.normalized * Settings.RadiusAttackPlayer;

        return targetPosition;
    }
    public void RotateZombie(BotController botController)
    {
        Vector3 direction = (PlayerManager.Instance.transform.position - botController._botController._rotateZombie.transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        float currentRotationX = botController._botController._rotateZombie.transform.rotation.eulerAngles.x;
        toRotation = Quaternion.Euler(currentRotationX, toRotation.eulerAngles.y, toRotation.eulerAngles.z);
        botController._botController._rotateZombie.transform.rotation = toRotation;
    }
    public void RotatePlayer()
    {
        Vector3 direction = (PlayerManager.Instance._playerController._listBot[0].transform.position - PlayerManager.Instance.transform.position).normalized;
        direction.y = 0;
        Quaternion newRotation = Quaternion.LookRotation(direction);

        PlayerManager.Instance._playerController._rotatePlayer.transform.rotation = newRotation;
        PlayerManager.Instance._playerController._directionBullet = direction;
        
    }
    #region Method Game

    public bool CheckUIReturn()
    {
        #region Kiểm tra xem có nhấn va UI nào không , nếu không thì return

#if UNITY_EDITOR || UNITY_STANDALONE
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject selectedObj = EventSystem.current.currentSelectedGameObject;
            if (selectedObj != null)
            {
                return true;
            }
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                GameObject selectedObj = EventSystem.current.currentSelectedGameObject;
                if (selectedObj != null)
                {
                    return true;
                }
            }
        }
#endif

        #endregion


        return false;
    }

    #endregion
}
