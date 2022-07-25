using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCollectedState : CollectableBaseState
{
    public CollectableCollectedState(CollectableController controller) : base(controller)
    {
    }

    public override void Update()
    {
        controller.MoveForward();
    }

    public override void FixedUpdate()
    {
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
                GameEvents.AddCollectableToStackEvent?.Invoke(other.transform);
            }
        }
    }
}
