using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public abstract class BaseClickButton : MonoBehaviour
{
    public Button myButton;

    protected abstract void OnButtonClick();

    private void Start()
    {
        myButton.onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        OnButtonClick();
    }

    
}
