using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
	public enum CollectableType
	{
		Currency,
		Stack
	}
	
    #region Variables

	// Public Variables
	[SerializeField] public int increaseValue = 10;

	// Private Variables
	[SerializeField] private CollectableType collectableType;

	private Transform _playerTrans;
	private CollectableBaseState _currentState;
	
	private float _startY, _zOffset;

	#endregion Variables

	private void Start()
	{
		_playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

		_currentState = new CollectableIdleState(this);
	}

	private void Update()
	{
		_currentState.Update();
	}

	private void OnTriggerEnter(Collider other)
	{
		_currentState.OnTriggerEnter(other);
	}

	public void StartFunc()
	{
		_startY = transform.position.y;
		_zOffset = transform.position.z - _playerTrans.position.z;

		_currentState = new CollectableCollectedState(this);
	}
	
	public CollectableType GetCollectableType() => collectableType;

	public bool CheckCanAddToStack() => _currentState.GetType() == typeof(CollectableIdleState);

		public void MoveForward()
	{
		transform.position = new Vector3(transform.position.x, _startY, _playerTrans.position.z + _zOffset);
	}
}
