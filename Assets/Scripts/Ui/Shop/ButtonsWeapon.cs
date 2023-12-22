using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsWeapon : MonoBehaviour
{
    [SerializeField] Text indexWeapon;

    public void OnButtonClick()
    {
        Debug.Log(Int32.Parse(indexWeapon.text));
    }

}
