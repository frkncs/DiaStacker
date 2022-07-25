using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackSliderController : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	private SlicedFilledImage _slider;
	private PlayerStackController _playerStackController;

	#endregion Variables
    
	private void Start()
	{
		_slider = GetComponent<SlicedFilledImage>();
		_playerStackController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStackController>();

		_slider.fillAmount = 0;
		
		UpdateSliderValue();
		
		GameEvents.UpdateStackItemCount += UpdateSliderValue;
	}

	private void UpdateSliderValue()
	{
		float value = 1 / ((float)_playerStackController.GetMaxStackObjectCount() / 
		                   _playerStackController.GetStackObjectCount());

		_slider.DOKill();
		
		DOTween.To(() => _slider.fillAmount, x => _slider.fillAmount = x, value, .2f)
			.SetEase(Ease.OutSine);
    }
}
