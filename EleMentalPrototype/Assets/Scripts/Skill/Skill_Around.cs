using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Around : Skill
{
    [SerializeField] Collider attackArangeCollider_;
    [SerializeField] Rigidbody attackArangeRigidbody_;
    [SerializeField] List<Collider> enterColliderList_ = new List<Collider>();
    
    public override void Init(SKILL_NAME skillName, Vector3 skillPosition, int damage, List<TEAM> targetTeamList, int hitCnt)
    {
        base.Init(skillName, skillPosition, damage, targetTeamList, hitCnt);
        
        enterColliderList_.Clear();
        attackArangeCollider_.enabled = true;
    }

    protected override void SkillUpdate()
    {
        base.SkillUpdate();

        attackArangeRigidbody_.WakeUp();

        elapsedDelay_ += Time.smoothDeltaTime;

        if(elapsedDelay_ >= hitDelay_)
        {
            TargetUnitSetting();
            HitDamage();
            elapsedDelay_ -= hitDelay_;
        }
    }

    protected override void TargetUnitSetting()
    {
        base.TargetUnitSetting();

        targetUnitList_.Clear();
        
        for (int i = 0; i < enterColliderList_.Count; ++i)
        {
            UnitCollider unitCollider = enterColliderList_[i].GetComponent<UnitCollider>();
            if(unitCollider == null)
                continue;

            Unit unit = unitCollider.OwnUnit;

            if (targetTeamList_.Contains(unit.Team) == false)
            {
                continue;
            }
            
            targetUnitList_.Add(unit);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enterColliderList_.Contains(other))
            return;

        enterColliderList_.Add(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        //enterColliderList_.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        //enterColliderList_.Remove(other);
    }

    protected override void RestoreSkill()
    {
        attackArangeCollider_.enabled = false;

        base.RestoreSkill();
    }
}
