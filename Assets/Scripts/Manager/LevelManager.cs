using Ring;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : RingSingleton<LevelManager>
{
    [HeaderTextColor(0.2f, .7f, .8f, headerText = "Level in Game")] public LevelController _levelController;
    public void AddLevel(GameObject level)
    {
        _levelController._listLevel.Add(level);
    }
}
