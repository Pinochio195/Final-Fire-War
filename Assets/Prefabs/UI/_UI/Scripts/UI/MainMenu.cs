using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    [SerializeField] GameObject Level;
    [SerializeField] GameObject Loading;
    [SerializeField] GameObject Home;
    public void StartGame()
    {
        Loading.SetActive(true);
        Home.SetActive(false);
    }

    public void Settings()
    {
        UiManager.Instance.OpenUI<Setting>();//nhảy ra màn home
        Close();
    }
}
