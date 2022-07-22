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
	[SerializeField] private Transform modelTransform;
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
	    if (moveType == MoveType.None)
	    {
		    modelTransform.Rotate(Vector3.forward * (Time.deltaTime * 400f));
	    }
	    else if (moveType == MoveType.Horizontal)
	    {
		    if (moveReverse)
		    {
			    transform.position += -transform.right * (Time.deltaTime * moveSpeed);
			    modelTransform.Rotate(Vector3.forward * (Time.deltaTime * 400f));
		    }
		    else
		    {
			    transform.position += transform.right * (Time.deltaTime * moveSpeed);
			    modelTransform.Rotate(-Vector3.forward * (Time.deltaTime * 400f));
		    }
	    
		    if (!Physics.Raycast(rayPos.transform.position, Vector3.down,10f, groundLayer))
		    {
			    moveReverse = !moveReverse;
		    }
	    }
	    else if (moveType == MoveType.Circular)
	    {
		    modelTransform.Rotate(Vector3.forward * (Time.deltaTime * 400f));
		    transform.position += -transform.right * (Time.deltaTime * moveSpeed);
		    
		    transform.Rotate(Vector3.up * (Time.deltaTime * 100f));
	    }
    }
}
