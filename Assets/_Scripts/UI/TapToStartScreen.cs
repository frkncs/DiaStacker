using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TapToStartScreen : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables

	#endregion Variables
    
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !Helper.IsOverUI())
        {
            GameEvents.GameStartedEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
