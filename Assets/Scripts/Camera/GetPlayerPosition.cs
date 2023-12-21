using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerPosition : MonoBehaviour
{
    [SerializeField]CinemachineVirtualCamera virtualCamera;
    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance!=null)
        {
            if (virtualCamera.Follow == null)
            {
                virtualCamera.Follow = PlayerManager.Instance._playerController._targetFollow.transform;
            }
        }
    }
}
