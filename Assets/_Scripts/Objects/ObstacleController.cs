using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	private enum MoveType
	{
		None,
		Horizontal,
		Circular
	}
	
    #region Variables

	// Public Variables

	// Private Variables
	[SerializeField] private Transform rayPos;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private MoveType moveType;
	[SerializeField] private float moveSpeed;

	private bool moveReverse;
	
	#endregion Variables
    
    private void Update()
    {
	    Move();
    }

    private void Move()
    {
	    if (moveType == MoveType.Horizontal)
	    {
		    if (moveReverse)
		    {
			    transform.position += -transform.right * (Time.deltaTime * moveSpeed);
		    }
		    else
		    {
			    transform.position += transform.right * (Time.deltaTime * moveSpeed);
		    }
	    
		    if (!Physics.Raycast(rayPos.transform.position, Vector3.down,10f, groundLayer))
		    {
			    moveReverse = !moveReverse;
		    }
	    }
	    else if (moveType == MoveType.Circular)
	    {
		    transform.position += -transform.right * (Time.deltaTime * moveSpeed);
		    
		    transform.Rotate(Vector3.up * (Time.deltaTime * 100f));
	    }
    }
}
