using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	[SerializeField] private GameObject winScreen;
	[SerializeField] private GameObject currentMoneyObject;
	[SerializeField] private TextMeshProUGUI txtCurrentLevel, txtCurrentMoney, txtCollectedMoney;

	#endregion Variables
    
	private void Start()
	{
		txtCurrentLevel.text = "Level " + (PlayerPrefs.GetInt("PlayerLevel") + 1);
		
		UpdateCurrentMoney();

		GameEvents.WinGameEvent += OnWinGame;
		GameEvents.UpdateCurrentMoneyEvent += UpdateCurrentMoney;
		GameEvents.SetCollectedMoney += SetCollectedMoneyText;
	}

    private void OnWinGame()
    {
        txtCurrentLevel.gameObject.SetActive(false);
        currentMoneyObject.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(true);
    }

    private void UpdateCurrentMoney()
    {
	    txtCurrentMoney.text = PlayerPrefs.GetInt("Money").ToString();
    }

    private void SetCollectedMoneyText(int collectedMoney)
    {
	    txtCollectedMoney.text = "+" + collectedMoney;
    }
}
