using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerStackController : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	[SerializeField] private Transform stackStartPos;
	[SerializeField] private GameObject stackItemCountObject;
	[SerializeField] private TextMeshProUGUI txtStackItemCount;
	[SerializeField] private int maxStackLimit = 20;

	private PlayerController _playerController;
	private List<CollectableController> _collectableControllers;

	private const float DistanceBetween2StackObj = .9f;
	
	#endregion Variables
    
	private void Awake()
	{
		_collectableControllers = new List<CollectableController>();
		_playerController = GetComponent<PlayerController>();
	}

	private void Start()
	{
		GameEvents.AddCollectableToStackEvent += AddCollectableToStack;
		GameEvents.RemoveCollectableFromStackEvent += RemoveCollectableFromStack;
		GameEvents.WinGameEvent += ThrowAllStack;
		GameEvents.WinGameEvent += () =>
		{
			stackItemCountObject.SetActive(false);
		};
	}

	private void Update()
	{
		MoveStack();
	}

	private void AddCollectableToStack(CollectableController collectableToAdd)
	{
		if (!collectableToAdd.CheckCanAddToStack()) return;
		if (_collectableControllers.Count >= maxStackLimit)
		{
			Destroy(collectableToAdd.gameObject);
			return;
		}

		var spawnPos = transform.position + (Vector3.forward * DistanceBetween2StackObj);
		spawnPos.y = stackStartPos.position.y;
		spawnPos.z = stackStartPos.position.z;
		
		if (_collectableControllers.Count != 0)
		{
			spawnPos.y = _collectableControllers.GetPeekObj().transform.position.y + DistanceBetween2StackObj;
		}
		
		collectableToAdd.transform.position = spawnPos;
		collectableToAdd.StartFunc();
		
		_collectableControllers.Add(collectableToAdd);

		txtStackItemCount.text = _collectableControllers.Count.ToString();
		
		if (_collectableControllers.Count == 1)
		{
			_playerController.PlayRun2Anim();
		}
	}

	private void RemoveCollectableFromStack()
	{
		if (_collectableControllers.Count <= 0) return;

		var collectableToRemove = _collectableControllers.GetPeekObj();
		collectableToRemove.ThrowCollectable();
		
		_collectableControllers.Remove(collectableToRemove);
		
		txtStackItemCount.text = _collectableControllers.Count.ToString();

		Destroy(collectableToRemove.gameObject, 2f);

		if (_collectableControllers.Count == 0)
		{
			_playerController.PlayRunAnim();
		}
	}

	private void ThrowAllStack()
	{
		int loopCount = _collectableControllers.Count;
		
		for (int i = 0; i < loopCount; i++)
		{
			RemoveCollectableFromStack();
		}
	}

	private void MoveStack()
	{
		for (int i = _collectableControllers.Count - 1; i >= 0; i--)
		{
			if (i == 0)
			{
				//_collectableControllers[i].transform.DOMoveX(transform.position.x, .1f);
				var movePos = _collectableControllers[i].transform.position;
				movePos.x = transform.position.x;
				
				_collectableControllers[i].transform.position = movePos;
			}
			else
			{
				_collectableControllers[i].transform.DOMoveX(_collectableControllers[i - 1].transform.position.x, .1f);
			}
		}
	}
}
