using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	[SerializeField] private ParticleSystem confetti01, confetti02;

	#endregion Variables

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			confetti01.Play();
			confetti02.Play();
			
			GameEvents.WinGameEvent?.Invoke();
		}
	}
}
