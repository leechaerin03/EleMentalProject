using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillComp_PushOut : SkillComponent
{
    Vector3 anchorUnitPosition_;
    float pushOutForce_;

    public void Init(Vector3 anchorUnitPosition, float pushOutForce)
    {
        anchorUnitPosition_ = anchorUnitPosition;
        pushOutForce_ = pushOutForce;

    }

    public override void PlayComponentAction()
    {
        base.PlayComponentAction();

        for(int i = 0; i < targetUnitList_.Count; ++i)
        {
            Unit targetUnit = targetUnitList_[i];
            Vector3 diff = targetUnit.UnitPosition - anchorUnitPosition_;
            Vector3 normalVector = Vector3.Normalize(diff);

            targetUnit.Rigidbody.AddForce(normalVector * pushOutForce_);
        }
    }
}
