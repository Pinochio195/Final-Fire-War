using System;
using Ring;

public class EventManager : RingSingleton<EventManager>
{
    public Action OnWinGame;

    public void Action_WinGame()
    {
        OnWinGame?.Invoke();
    }
}
