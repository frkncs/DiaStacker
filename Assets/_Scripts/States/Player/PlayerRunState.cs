using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerController controller) : base(controller)
    {
        if (controller.playerStackController.collectableControllers.Count > 0)
        {
            controller.PlayRun2Anim(1);
        }
        else
        {
            controller.PlayRunAnim();   
        }
    }

    public override void Update()
    {
        controller.swerveMovement.OnUpdate();
    }

    public override void FixedUpdate()
    {
        controller.swerveMovement.OnFixedUpdate();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            var collectableController = other.GetComponent<CollectableController>();

            if (collectableController.GetCollectableType() == CollectableController.CollectableType.Currency)
            {
                GameEvents.AddGoldToCurrencyEvent?.Invoke(collectableController);
            }
            else if (collectableController.GetCollectableType() == CollectableController.CollectableType.Stack)
            {
                GameEvents.AddCollectableToStackEvent?.Invoke(collectableController);
            }
        }
        else if (other.CompareTag("Obstacle"))
        {
            GameEvents.PlayHittedObstacleFeedbackEvent?.Invoke();
            
            for (int i = 0; i < 2; i++)
            {
                GameEvents.RemoveCollectableFromStackEvent?.Invoke();
            }
        }
    }
}
