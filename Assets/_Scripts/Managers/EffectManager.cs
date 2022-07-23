using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [Header("Feedbacks")] [SerializeField] private MMFeedbacks collectedFeedback;
    [SerializeField] private MMFeedbacks hittedObstacleFeedback;

    [Header("Particles")]
    [SerializeField] private ParticleSystem collectedDiamondParticle;
    [SerializeField] private ParticleSystem collectedGoldParticle;

    #endregion Variables

    private void Start()
    {
        GameEvents.PlayCollectedFeedbackEvent += PlayCollectedFeedback;
        GameEvents.PlayHittedObstacleFeedbackEvent += PlayHittedObstacleFeedback;
    }

    private void PlayCollectedFeedback(CollectableController.CollectableType collectableType, Vector3 pos)
    {
        if (collectableType == CollectableController.CollectableType.Currency)
        {
            collectedGoldParticle.transform.position = pos;
            collectedGoldParticle.Play();
        }
        else if (collectableType == CollectableController.CollectableType.Stack)
        {
            collectedDiamondParticle.transform.position = pos;
            collectedDiamondParticle.Play();
        }

        collectedFeedback.PlayFeedbacks();
    }

    private void PlayHittedObstacleFeedback() => hittedObstacleFeedback.PlayFeedbacks();
}