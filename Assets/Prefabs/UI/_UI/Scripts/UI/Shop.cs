using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : UICanvas
{
    public void Exit()
    {
        Close();
    }
    public void NextGame()
    {
        //UiManager.Instance.OpenUI<GamePlay>();
        Close();
    }
}
