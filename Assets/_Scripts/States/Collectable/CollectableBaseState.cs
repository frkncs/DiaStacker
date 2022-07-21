using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableBaseState
{
    protected CollectableController controller;

    public CollectableBaseState(CollectableController controller)
    {
        this.controller = controller;
    }

    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void OnTriggerEnter(Collider other);
}
