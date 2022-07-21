using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	private Transform _playerTrans;

	#endregion Variables
    
	private void Start()
	{
		_playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
	}

    private void FixedUpdate()
    {
	    var movePos = transform.position;
	    movePos.z = _playerTrans.position.z;
	    
	    transform.position = movePos;
    }
}
