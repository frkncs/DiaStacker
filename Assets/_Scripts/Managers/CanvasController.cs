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
		txtCurrentMoney.text = PlayerPrefs.GetInt("Money").ToString();

		GameEvents.WinGameEvent += OnWinGame;
	}

    private void OnWinGame()
    {
        txtCurrentLevel.gameObject.SetActive(false);
        txtCurrentMoney.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(true);
    }
}
