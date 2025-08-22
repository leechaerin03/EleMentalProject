using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] float slillAliveTime_ = 1f;
    [SerializeField] protected float hitDelay_;
    [SerializeField] ELEMENT element_;

    protected float elapsedDelay_;
    float skillElapsedTime_;

    protected Vector3 createdPosition_;

    SKILL_NAME skillName_;
    bool isAlive_;
    int damage_;
    int hitCnt_;


    protected List<TEAM> targetTeamList_;
    protected List<Unit> targetUnitList_ = new List<Unit>();

    protected List<SkillComponent> skillComponentList_ = new List<SkillComponent>();

    public virtual void Init(SKILL_NAME skillName, Vector3 skillPosition, int damage, List<TEAM> targetTeamList, int hitCnt = 1)
    {
        skillName_ = skillName;
        isAlive_ = true;
        damage_ = damage;
        hitCnt_ = hitCnt;

        SkillPosition = skillPosition;
        createdPosition_ = skillPosition;

        targetTeamList_ = targetTeamList;

        elapsedDelay_ = 0f;
        skillElapsedTime_ = 0f;

        skillComponentList_.Clear();

        SkillPosition = new Vector3(SkillPosition.x, SkillDefine.mapSkillCreateYPosition_[skillName], SkillPosition.z);
    }

    public virtual void Init(SKILL_NAME skillName, Vector3 skillPosition, int damage, List<TEAM> targetTeamList, float hitDelay)
    {
        Init(skillName, skillPosition, damage, targetTeamList);
    }

    public virtual void AddSkillComponent(SkillComponent skillComponent)
    {
        skillComponentList_.Add(skillComponent);
    }

    public virtual void AddSkillComponent(List<SkillComponent> skillComponentList)
    {
        for(int i = 0; i < skillComponentList.Count; ++i)
        {
            AddSkillComponent(skillComponentList[i]);
        }
    }

    private void Update()
    {
        if (isAlive_ == false)
            return;

        SkillUpdate();

        skillElapsedTime_ += Time.smoothDeltaTime;
        if(skillElapsedTime_ >= slillAliveTime_)
        {
            RestoreSkill();
        }
    }

    protected virtual void SkillUpdate()
    {

    }

    protected virtual void TargetUnitSetting()
    {

    }

    protected virtual void HitDamage()
    {
        if (hitCnt_  <= 0)
            return;

        for (int i = 0; i < targetUnitList_.Count; ++i)
        {
            targetUnitList_[i].HitDmg(damage_, element_);
        }
        hitCnt_ -= 1;

        for(int i = 0; i < skillComponentList_.Count; ++i)
        {
            SkillComponent skillComponent = skillComponentList_[i];
            skillComponent.SetTargetUnitList(targetUnitList_);

            skillComponentList_[i].PlayComponentAction();
        }
    }

    protected virtual void RestoreSkill()
    {
        isAlive_ = false;

        Hub_Ingame.Instance.skillController_.RestoreSkill(this);
    }

    public SKILL_NAME SkillName
    {
        get { return skillName_; }
    }

    public Vector3 SkillPosition
    {
        get { return transform.localPosition; }
        set { transform.localPosition = value; }
    }
}
