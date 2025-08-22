using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ScaleTo : ActionInterface
{
    [SerializeField] Vector3 startScale_;
    [SerializeField] Vector3 targetScale_;

    public override void Init()
    {
        base.Init();

        transform.localScale = startScale_;
    }

    protected override void ActionUpdate()
    {
        base.ActionUpdate();

        if (elapsedTime_ < 0)
            return;

        transform.localScale = startScale_ + (targetScale_ - startScale_) * (elapsedTime_ / durationTime_);

    }
}
