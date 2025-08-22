using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillComp_Stun : SkillComponent
{
    float stunTime_;
    
    public void Init(float stunTime)
    {
        stunTime_ = stunTime;
    }

    public override void PlayComponentAction()
    {
        base.PlayComponentAction();

        for (int i = 0; i < targetUnitList_.Count; ++i)
        {
            Unit targetUnit = targetUnitList_[i];

            targetUnit.SetStunTime(stunTime_);
        }
    }
}
