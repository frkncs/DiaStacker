using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerController controller;

    public PlayerBaseState(PlayerController controller)
    {
        this.controller = controller;
    }

    public abstract void Update();
    public abstract void FixedUpdate();
}
