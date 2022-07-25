using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helper
{
	public static T GetPeekObj<T>(this List<T> list)
	{
		return list[list.Count - 1];
	}
	
	public static void PlayScaleBounceEffect(this Transform transform, Vector3 defScale)
	{
		transform.DOKill();
		transform.localScale = defScale;

		transform.DOScale(transform.localScale * 1.4f, .1f)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
			{
				transform.DOScale(defScale, .1f)
					.SetEase(Ease.Linear);
			});
	}
	
	private static PointerEventData _eventDataCurrentPosition;
	private static List<RaycastResult> _results;

	public static bool IsOverUI()
	{
		_eventDataCurrentPosition = new PointerEventData(EventSystem.current)
		{
			position = Input.mousePosition
		};

		_results = new List<RaycastResult>();

		EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);

		return _results.Count > 0;
	}
}
