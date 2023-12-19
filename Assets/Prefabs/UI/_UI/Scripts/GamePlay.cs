using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : UICanvas
{

    public void WinButton()
    {
        UiManager.Instance.OpenUI<Win>()._countKill.text = Random.Range(100, 200).ToString();
        Close();
    }

    public void LoseButton()
    {
        UiManager.Instance.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString(); 
        Close();
    }

    public void SettingButton()
    {
        UiManager.Instance.OpenUI<Setting>();
    }
    public void PlayGame()
    {
        UiManager.Instance.OpenUI<Setting>();
    }
}
