using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action GameStartedEvent;
    public static Action WinGameEvent;
    public static Action<CollectableController> AddCollectableToStackEvent;
    public static Action<CollectableController> AddGoldToCurrencyEvent;
    public static Action UpdateCurrentMoneyEvent;
    
    private void OnDestroy()
    {
        DestroyEvents();
    }

    private void DestroyEvents()
    {
        GameStartedEvent = null;
        WinGameEvent = null;
        AddCollectableToStackEvent = null;
        AddGoldToCurrencyEvent = null;
        UpdateCurrentMoneyEvent = null;
    }
}
