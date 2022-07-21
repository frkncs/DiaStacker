using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables

	#endregion Variables
    
	private void Start()
	{
		GameEvents.AddGoldToCurrencyEvent += AddGoldToCurrency;
	}

    private void AddGoldToCurrency(CollectableController goldCollectableController)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + goldCollectableController.increaseValue);
        
        GameEvents.UpdateCurrentMoneyEvent?.Invoke();
        
        Destroy(goldCollectableController.gameObject);
    }
}
