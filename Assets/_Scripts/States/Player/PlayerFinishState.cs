using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinishState : PlayerBaseState
{
    public PlayerFinishState(PlayerController controller) : base(controller)
    {
        controller.PlayDanceAnim();
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider other)
    {
    }
}
