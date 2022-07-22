using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackCounter : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	private Transform _playerTransform;

	#endregion Variables
    
	private void Awake()
	{
		_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

    private void Update()
    {
	    transform.DOMoveX(_playerTransform.position.x + 1f, .2f);
	    transform.position = new Vector3(transform.position.x, transform.position.y, _playerTransform.position.z);
    }
}
