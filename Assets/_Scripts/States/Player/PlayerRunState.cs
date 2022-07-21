using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerController controller) : base(controller)
    {
        controller.PlayRunAnim();
    }

    public override void Update()
    {
        controller.swerveMovement.OnUpdate();
    }

    public override void FixedUpdate()
    {
        controller.swerveMovement.OnFixedUpdate();
    }
}
