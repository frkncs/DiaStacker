using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StackCounterText : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	[SerializeField] private TextMeshProUGUI txtStackItemCount;

	private PlayerStackController _playerStackController;

	private Image backgroundImage;
	private Vector3 _stackItemCountObjectDefScale;
	
	#endregion Variables
    
	private void Awake()
	{
		backgroundImage = GetComponent<Image>();
	    _stackItemCountObjectDefScale = transform.localScale;
	    
	    _playerStackController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStackController>();
	}

	private void Start()
	{
		GameEvents.GameStartedEvent += () =>
		{
			GameEvents.UpdateStackItemCount += UpdateStackItemCountText;
			UpdateStackItemCountText();
		};
	}

	private void UpdateStackItemCountText()
	{
		txtStackItemCount.text = _playerStackController.GetStackObjectCount().ToString();

		transform.PlayScaleBounceEffect(_stackItemCountObjectDefScale);
	}
}
