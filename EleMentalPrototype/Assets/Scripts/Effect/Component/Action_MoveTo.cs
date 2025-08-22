using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_MoveTo : ActionInterface
{
    [SerializeField] Vector3 startPosition_;
    [SerializeField] Vector3 targetPosition_;

    public override void Init()
    {
        base.Init();

        transform.localPosition = startPosition_;
    }

    protected override void ActionUpdate()
    {
        base.ActionUpdate();

        if (elapsedTime_ < 0)
            return;

        transform.localPosition = startPosition_ + (targetPosition_ - startPosition_) * (elapsedTime_ / durationTime_);

    }
}
