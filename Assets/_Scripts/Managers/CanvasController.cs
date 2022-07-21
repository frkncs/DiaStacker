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
	[SerializeField] private TextMeshProUGUI txtCurrentLevel, txtCurrentMoney;

	#endregion Variables
    
	private void Start()
	{
		txtCurrentLevel.text = "Level " + (PlayerPrefs.GetInt("PlayerLevel") + 1);
		
		UpdateCurrentMoney();

		GameEvents.WinGameEvent += OnWinGame;
		GameEvents.UpdateCurrentMoneyEvent += UpdateCurrentMoney;
	}

    private void OnWinGame()
    {
        txtCurrentLevel.gameObject.SetActive(false);
        txtCurrentMoney.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(true);
    }

    private void UpdateCurrentMoney()
    {
	    txtCurrentMoney.text = PlayerPrefs.GetInt("Money").ToString();
    }
}
