using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct GameEvents
{
    public static Action GameStartedEvent;
    public static Action WinGameEvent;
    public static Action<Transform> AddCollectableToStackEvent;
    public static Action<CollectableController> AddGoldToCurrencyEvent;
    public static Action UpdateCurrentMoneyEvent;
    public static Action UpdateStartStackItemCount;
    public static Action RemoveCollectableFromStackEvent;
    public static Action<int> SetCollectedMoney;
    public static Action UpdateStackItemCount;
    
    public static Action<CollectableController.CollectableType, Vector3> PlayCollectedFeedbackEvent;
    public static Action PlayHittedObstacleFeedbackEvent;
    
    public static void DestroyEvents()
    {
        GameStartedEvent = null;
        WinGameEvent = null;
        AddCollectableToStackEvent = null;
        AddGoldToCurrencyEvent = null;
        UpdateCurrentMoneyEvent = null;
        UpdateStartStackItemCount = null;
        RemoveCollectableFromStackEvent = null;
        SetCollectedMoney = null;
        UpdateStackItemCount = null;
        
        PlayCollectedFeedbackEvent = null;
        PlayHittedObstacleFeedbackEvent = null;
    }
}

public class LevelManager : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables

	#endregion Variables
    
	private void Awake()
    {
        var levelIndex  = PlayerPrefs.GetInt("PlayerLevel") % SceneManager.sceneCountInBuildSettings;

        if (SceneManager.GetActiveScene().buildIndex != levelIndex)
        {
            SceneManager.LoadScene(levelIndex);   
        }
    }

    private void OnDestroy()
    {
        GameEvents.DestroyEvents();
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);

        var level = PlayerPrefs.GetInt("PlayerLevel");

        SceneManager.LoadScene(level % SceneManager.sceneCountInBuildSettings);
    }
}
