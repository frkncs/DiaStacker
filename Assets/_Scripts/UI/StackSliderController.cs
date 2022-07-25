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

	private Vector3 _sliderDefScale;
	
	#endregion Variables
    
	private void Start()
	{
		_slider = GetComponent<SlicedFilledImage>();
		_sliderDefScale = _slider.transform.parent.localScale;
		_playerStackController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStackController>();

		_slider.fillAmount = 0;
		
		UpdateSliderValue();
		
		GameEvents.UpdateStackItemCount += UpdateSliderValue;
	}

	private void UpdateSliderValue()
	{
		float value = 1 / ((float)_playerStackController.GetMaxStackObjectCount() / 
		                   _playerStackController.GetStackObjectCount());

		_slider.transform.parent.PlayScaleBounceEffect(_sliderDefScale);
		
		_slider.DOKill();

		_slider.DOColor(value >= _slider.fillAmount ? Color.green : Color.red, .15f)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
			{
				_slider.DOColor(Color.white, .15f)
					.SetEase(Ease.OutSine);
			});
		
		DOTween.To(() => _slider.fillAmount, x => _slider.fillAmount = x, value, .2f)
			.SetEase(Ease.OutSine);
	}
}
