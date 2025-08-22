using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Projectile : Skill
{
    [SerializeField] float projectileFlyTime_;
    [SerializeField] int targetUnitCnt_;
    [SerializeField] EFFECT_NAME projectileEffectName_;
    [SerializeField] Effect_Projectile.PROJECTILE_TYPE projectileType_;


    public override void Init(SKILL_NAME skillName, Vector3 skillPosition, int damage, List<TEAM> targetTeamList, int hitCnt)
    {
        base.Init(skillName, skillPosition, damage, targetTeamList, hitCnt);
        
        ProjectileShoot();
    }

    void ProjectileShoot()
    {
        TargetUnitSetting();
        
        // Projectile Effect Create
        for(int i = 0; i < targetUnitList_.Count; ++i)
        {
            Hub_Ingame.Instance.effectController_.UseEffect(projectileEffectName_, SkillPosition, targetUnitList_[i].transform, projectileFlyTime_, projectileType_);
        }
    }

    protected override void SkillUpdate()
    {
        base.SkillUpdate();

        elapsedDelay_ += Time.smoothDeltaTime;

        if (elapsedDelay_ >= hitDelay_)
        {
            HitDamage();
            elapsedDelay_ -= hitDelay_;
        }
    }

    protected override void TargetUnitSetting()
    {
        base.TargetUnitSetting();

        targetUnitList_ = Hub_Ingame.Instance.unitController_.FindNearUnitListNullable(targetTeamList_, createdPosition_, targetUnitCnt_);
    }
}
