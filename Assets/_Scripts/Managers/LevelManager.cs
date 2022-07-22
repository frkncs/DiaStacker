using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct GameEvents
{
    public static Action GameStartedEvent;
    public static Action WinGameEvent;
    public static Action<CollectableController> AddCollectableToStackEvent;
    public static Action<CollectableController> AddGoldToCurrencyEvent;
    public static Action UpdateCurrentMoneyEvent;
    public static Action RemoveCollectableFromStackEvent;
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
        DestroyEvents();
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);

        var level = PlayerPrefs.GetInt("PlayerLevel");

        SceneManager.LoadScene(level % SceneManager.sceneCountInBuildSettings);
    }
    
    private void DestroyEvents()
    {
        GameEvents.GameStartedEvent = null;
        GameEvents.WinGameEvent = null;
        GameEvents.AddCollectableToStackEvent = null;
        GameEvents.AddGoldToCurrencyEvent = null;
        GameEvents.UpdateCurrentMoneyEvent = null;
        GameEvents.RemoveCollectableFromStackEvent = null;
    }
}
