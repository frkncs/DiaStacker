using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController controller) : base(controller)
    {
        controller.PlayIdleAnim();
    }

    public override void Update()
    {
        if (Input.GetMouseButtonUp(0) && !Helper.IsOverUI())
        {
            controller.RunState();
        }
    }

    public override void FixedUpdate()
    {
    }
}
