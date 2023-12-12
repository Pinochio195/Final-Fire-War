using Cinemachine;
using UnityEngine;

public class AimPlayer : BaseClickButton
{
    public bool isCheck;
    public bool isCheckBlend;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private GameObject _cameraPlayer;
    [SerializeField] private GameObject _cameraFire;
    private void Update()
    {
        if (!isCheckBlend && cinemachineBrain.ActiveBlend != null)
        {
            Debug.Log("A");
            isCheckBlend = true;
        }
        else if(isCheckBlend && cinemachineBrain.ActiveBlend == null && !myButton.interactable)
        {
            Debug.Log("B");
            isCheckBlend = false;
            myButton.interactable = true;
        }
    }
    protected override void OnButtonClick()
    {
        _cameraPlayer.SetActive(isCheck);
        _cameraFire.SetActive(!isCheck);
        isCheck = !isCheck;
        myButton.interactable = false;
    }
}