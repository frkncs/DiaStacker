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
	[HideInInspector] public List<CollectableController> collectableControllers;

	// Private Variables
	[SerializeField] private CollectableController collectablePrefab;
	[SerializeField] private Transform stackStartPos;
	[SerializeField] private GameObject stackItemCountObject;
	[SerializeField] private TextMeshProUGUI txtStackItemCount;
	[SerializeField] private int maxStackLimit = 20;

	private PlayerController _playerController;

	private const float DistanceBetween2StackObj = .9f;
	
	#endregion Variables
    
	private void Awake()
	{
		collectableControllers = new List<CollectableController>();
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

		GameEvents.UpdateStartStackEvent += UpdateStartStack;
		
		UpdateStartStack();
	}

	private void Update()
	{
		MoveStack();
	}

	private void AddCollectableToStack(CollectableController collectableToAdd)
	{
		if (!collectableToAdd.CheckCanAddToStack()) return;
		if (collectableControllers.Count >= maxStackLimit)
		{
			Destroy(collectableToAdd.gameObject);
			return;
		}

		var spawnPos = transform.position + (Vector3.forward * DistanceBetween2StackObj);
		spawnPos.y = stackStartPos.position.y;
		spawnPos.z = stackStartPos.position.z;
		
		if (collectableControllers.Count != 0)
		{
			spawnPos.y = collectableControllers.GetPeekObj().transform.position.y + DistanceBetween2StackObj;
		}
		
		collectableToAdd.transform.position = spawnPos;
		collectableToAdd.StartFunc();
		
		collectableControllers.Add(collectableToAdd);

		txtStackItemCount.text = collectableControllers.Count.ToString();
		
		if (collectableControllers.Count == 1)
		{
			if (_playerController.CheckIsIdleState())
			{
				_playerController.PlayRun2Anim(0);
			}
			else
			{
				_playerController.PlayRun2Anim(1);	
			}
		}
	}

	private void RemoveCollectableFromStack()
	{
		if (collectableControllers.Count <= 0) return;

		var collectableToRemove = collectableControllers.GetPeekObj();
		collectableToRemove.ThrowCollectable();
		
		collectableControllers.Remove(collectableToRemove);
		
		txtStackItemCount.text = collectableControllers.Count.ToString();

		Destroy(collectableToRemove.gameObject, 2f);

		if (collectableControllers.Count == 0)
		{
			_playerController.PlayRunAnim();
		}
	}

	private void UpdateStartStack()
	{
		foreach (var collectableController in collectableControllers)
		{
			Destroy(collectableController.gameObject);
		}
		
		collectableControllers.Clear();

		for (int i = 0; i < PlayerPrefs.GetInt("StartStackCount"); i++)
		{
			AddCollectableToStack(Instantiate(collectablePrefab));
		}
	}

	private void ThrowAllStack()
	{
		int loopCount = collectableControllers.Count;
		
		for (int i = 0; i < loopCount; i++)
		{
			RemoveCollectableFromStack();
		}
	}

	private void MoveStack()
	{
		for (int i = collectableControllers.Count - 1; i >= 0; i--)
		{
			if (i == 0)
			{
				//_collectableControllers[i].transform.DOMoveX(transform.position.x, .1f);
				var movePos = collectableControllers[i].transform.position;
				movePos.x = transform.position.x;
				
				collectableControllers[i].transform.position = movePos;
			}
			else
			{
				collectableControllers[i].transform.DOMoveX(collectableControllers[i - 1].transform.position.x, .1f);
			}
		}
	}
}
