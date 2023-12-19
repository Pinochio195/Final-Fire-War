using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : UICanvas
{
    public void ContinueButton()
    {
        UiManager.Instance.OpenUI<GamePlay>();
        Close();
    }
}
