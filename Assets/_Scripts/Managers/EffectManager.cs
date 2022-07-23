using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    #region Variables

	// Public Variables

	// Private Variables
	[Header("Feedbacks")]
	[SerializeField] private MMFeedbacks collectedFeedback;
	[SerializeField] private MMFeedbacks hittedObstacleFeedback;

	#endregion Variables
    
	private void Start()
	{
		GameEvents.PlayCollectedFeedbackEvent += PlayCollectedFeedback;
		GameEvents.PlayHittedObstacleFeedbackEvent += PlayHittedObstacleFeedback;
	}

	private void PlayCollectedFeedback() => collectedFeedback.PlayFeedbacks();
	private void PlayHittedObstacleFeedback() => hittedObstacleFeedback.PlayFeedbacks();
}
