using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    private int _collectedMoneyInLevel;

    #endregion Variables

    private void Start()
    {
        GameEvents.AddGoldToCurrencyEvent += AddGoldToCurrency;
        GameEvents.WinGameEvent += SetCollectedMoneyText;
    }
    
    private void AddGoldToCurrency(CollectableController goldCollectableController)
    {
        _collectedMoneyInLevel += goldCollectableController.increaseValue;

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + goldCollectableController.increaseValue);

        goldCollectableController.StartCollectedMoneyAnim();
        
        GameEvents.UpdateCurrentMoneyEvent?.Invoke();
        GameEvents.PlayCollectedFeedbackEvent?.Invoke(CollectableController.CollectableType.Currency, goldCollectableController.transform.position);

        Destroy(goldCollectableController.gameObject);
    }

    private void SetCollectedMoneyText()
    {
        GameEvents.SetCollectedMoney?.Invoke(_collectedMoneyInLevel);
    }
}