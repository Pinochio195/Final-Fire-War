using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadGun : BaseClickButton
{
    [SerializeField] Button_Fire _button_Fire;
    [SerializeField] AudioSource audio;
    protected override void OnButtonClick()
    {
        StartCoroutine(_button_Fire.ResetBullet(myButton));
        audio.Play();
    }
}
