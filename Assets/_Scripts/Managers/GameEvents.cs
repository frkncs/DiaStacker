using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action GameStartedEvent;
    public static Action WinGameEvent;
    
    private void OnDestroy()
    {
        DestroyEvents();
    }

    private void DestroyEvents()
    {
        GameStartedEvent = null;
        WinGameEvent = null;
    }
}
