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
    #region Save Data
    public int GetWeapon()
    {
        return PlayerPrefs.GetInt(Settings.Save_Weapon,0);
    }
    public void SetWeapon(int id_Weapon)
    {
        PlayerPrefs.GetInt(Settings.Save_Weapon, id_Weapon);
    }

    public int GetSound()
    {
        return PlayerPrefs.GetInt(Settings.Save_Sound, 1);
    }
    public void SetSound(int isCheckSound)
    {
        PlayerPrefs.SetInt(Settings.Save_Sound, isCheckSound);
    }

    public int GetVibrate()
    {
        return PlayerPrefs.GetInt(Settings.Save_Vibrate, 1);
    }
    public void SetVibrate(int isCheckSound)
    {
        PlayerPrefs.SetInt(Settings.Save_Vibrate, isCheckSound);
    }
    #endregion
}
