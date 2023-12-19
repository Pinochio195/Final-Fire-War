using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        //UiManager.Instance.OpenUI<GamePlay>();
        UiManager.Instance.OpenUI<Tutorials>();
        Close();
    }
}
