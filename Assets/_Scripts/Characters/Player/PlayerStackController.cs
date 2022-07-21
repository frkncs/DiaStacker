using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerStackController : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	private List<CollectableController> _collectableControllers;

	private const float DistanceBetween2StackObj = 1.2f;
	
	#endregion Variables
    
	private void Awake()
	{
		_collectableControllers = new List<CollectableController>();
	}

	private void Start()
	{
		GameEvents.AddCollectableToStackEvent += AddCollectableToStack;
	}

	private void Update()
	{
		MoveStack();
	}

	private void AddCollectableToStack(CollectableController collectableToAdd)
	{
		if (!collectableToAdd.CheckCanAddToStack()) return;
		
		var spawnPos = transform.position + (Vector3.forward * DistanceBetween2StackObj);
		spawnPos.y = 1f;
		
		if (_collectableControllers.Count != 0)
		{
			spawnPos.z = _collectableControllers.GetPeekObj().transform.position.z + DistanceBetween2StackObj;
		}
		
		collectableToAdd.transform.position = spawnPos;
		collectableToAdd.StartFunc();
		
		_collectableControllers.Add(collectableToAdd);
	}

	private void MoveStack()
	{
		for (int i = _collectableControllers.Count - 1; i >= 0; i--)
		{
			if (i == 0)
			{
				_collectableControllers[i].transform.DOMoveX(transform.position.x, .1f);
			}
			else
			{
				_collectableControllers[i].transform.DOMoveX(_collectableControllers[i - 1].transform.position.x, .1f);
			}
		}
	}
}
