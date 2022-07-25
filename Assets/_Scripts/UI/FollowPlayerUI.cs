using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FollowPlayerUI : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	[SerializeField] private float xOffset;
	
	private Transform _playerTransform;

	#endregion Variables
    
	private void Awake()
	{
		_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Start()
	{
		GameEvents.WinGameEvent += () => gameObject.SetActive(false);
	}

	private void Update()
	{
		transform.DOMoveX(_playerTransform.position.x + xOffset, .2f);
		transform.position = new Vector3(transform.position.x, transform.position.y, _playerTransform.position.z);
	}
}
