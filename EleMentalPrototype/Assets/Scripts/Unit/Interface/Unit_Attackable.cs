using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackData
{
    int damage_;
    public int Damage
    {
        get { return damage_; }
    }

    float arange_;
    public float Arange
    {
        get { return arange_; }
    }

    float attackDelay_;
    public float AttackDelay
    {
        get { return attackDelay_; }
    }

    float elapsedAttackDelay_;
    public float ElapsedAttackDelay
    {
        get { return elapsedAttackDelay_; }
        set { elapsedAttackDelay_ = value; }
    }

    ELEMENT atkElement_;
    public ELEMENT AtkElement
    {
        get { return atkElement_; }
        set { atkElement_ = value; }
    }

    public UnitAttackData(int damage, float arange, float attackDelay, ELEMENT atkElement = ELEMENT.NONE)
    {
        damage_ = damage;
        arange_ = arange;
        attackDelay_ = attackDelay;
        elapsedAttackDelay_ = 0f;
        atkElement_ = atkElement;
    }
}

public class Unit_Attackable : Unit
{
    [SerializeField] float attackHitDelayTime_ = 0.1f;
    protected float attackFlyDelayTime_ = 0f;
    protected UnitAttackData unitAttackData_;

    protected Unit nearEnemyUnit_ = null;

    public override void Init(UnitData unitData, UnitAttackData unitAttackData)
    {
        base.Init(unitData);

        unitAttackData_ = unitAttackData;
    }

    protected override void UnitUpdate()
    {
        base.UnitUpdate();
        
        rigidbody_.velocity = Vector3.MoveTowards(rigidbody_.velocity, Vector3.zero, 30f * Time.smoothDeltaTime);

        unitAttackData_.ElapsedAttackDelay += Time.smoothDeltaTime;

        // 목표 유닛이 없는 경우
        if(nearEnemyUnit_ == null || nearEnemyUnit_.IsAlive == false)
        {
            nearEnemyUnit_ = Hub_Ingame.Instance.unitController_.FindNearEnemyUnitNullable(Team, UnitPosition);
        }
        // 공격 중 목표 유닛이 사거리 밖으로 벗어낫을 경우 재탐색, 광란으로 만드려면 해당 분기 제거
        else if(UnitUtils.IsArangeIn(this, nearEnemyUnit_, unitAttackData_.Arange) == false)
        {
            nearEnemyUnit_ = Hub_Ingame.Instance.unitController_.FindNearEnemyUnitNullable(Team, UnitPosition);
        }

        if(nearEnemyUnit_ == null) // 적 유닛이 없는 경우
        {
            Stop(true);
            return;
        }

        if(UnitUtils.IsArangeIn(this, nearEnemyUnit_, unitAttackData_.Arange) == true)
        {
            Stop();
            AttackNearEnemy();
        }
        else
        {
            Move(nearEnemyUnit_.UnitPosition);
        }
    }

    protected override void Move(Vector3 targetUnitPosition)
    {
        base.Move(targetUnitPosition);

        RotateTargetUnit(targetUnitPosition);
    }

    protected bool IsAttackable(Unit targetUnit)
    {
        if (IsStun)
            return false;

        // 해당 함수는 사거리에 들어온 것을 전제로 공격가능한지 체크하는 것이기 때문에 사거리 체크 x
        if (nearEnemyUnit_ != null && 
            nearEnemyUnit_.IsAlive == true &&
            unitAttackData_.ElapsedAttackDelay >= unitAttackData_.AttackDelay)
        {
            return true;
        }

        return false;
    }

    protected void AttackNearEnemy()
    {
        RotateTargetUnit(nearEnemyUnit_.UnitPosition);

        if (IsAttackable(nearEnemyUnit_) == true)
        {
            Stop(true);
            
            unitAttackData_.ElapsedAttackDelay = 0f;
            
            AttackEffectCreate();
            
            SetUnitStatus(UNIT_STATUS.ATTACK);
        }
        else
        {
            nearEnemyUnit_ = null;
        }

    }

    void RotateTargetUnit(Vector3 targetUnitPosition)
    {
        if (CameraHolder.IsExistHoldCamera())
            return;

        Vector3 diff = targetUnitPosition - UnitPosition;
        float angle = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg - 90;

        cameraHolder_.GetUnitModule_AngleX_Transform().localEulerAngles = new Vector3(0, -angle, 0);
    }

    protected virtual void AttackEffectCreate()
    {
        nearEnemyUnit_.HitDmg(unitAttackData_.Damage, unitAttackData_.AtkElement, attackHitDelayTime_ + attackFlyDelayTime_);

    }
}
