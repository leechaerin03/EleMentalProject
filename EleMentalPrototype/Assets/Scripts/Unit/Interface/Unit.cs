using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitData
{
    float speed_;
    public float Speed
    {
        get { return speed_; }
    }

    int hp_;
    public int Hp
    {
        get { return hp_; }
    }

    int maxhp_;
    public int MaxHp
    {
        get { return maxhp_; }
    }

    TEAM team_;
    public TEAM Team
    {
        get { return team_; }
    }

    UNIT_NAME unitName_;
    public UNIT_NAME UnitName
    {
        get { return unitName_; }
    }

    float hitArange_;
    public float HitArange
    {
        get { return hitArange_; }
        set { hitArange_ = value; }
    }

    ELEMENT defElement_;
    public ELEMENT DefElement
    {
        get { return defElement_; }
    }

    float stunTime_;
    public float StunTime
    {
        get { return stunTime_; }
        set { stunTime_ = value; }
    }

    bool targetable_;

    public UnitData(UNIT_NAME unitName, float speed, int hp, TEAM team, ELEMENT defElement = ELEMENT.NONE, bool targetable = true)
    {
        unitName_ = unitName;
        speed_ = speed;
        hp_ = hp;
        maxhp_ = hp;
        team_ = team;
        defElement_ = defElement;

        stunTime_ = 0f;
        targetable_ = targetable;
    }

    public void AddHp(int hp)
    {
        hp_ += hp;
    }

    public bool IsDead()
    {
        return hp_ <= 0;
    }
}

public class Unit : MonoBehaviour
{
    [SerializeField] protected List<Renderer> teamColorRenderer_;
    [SerializeField] protected Rigidbody rigidbody_;
    [SerializeField] protected CameraHolder cameraHolder_;
    [SerializeField] protected UnitHPGauge unitHpGauge_;
    [SerializeField] protected Animator unitAnimator_;

    protected UnitData unitData_;
    protected Action deadCallback_;

    UNIT_STATUS unitStatus_;
    protected UNIT_STATUS UnitStatus
    {
        get { return unitStatus_; }
    }
    protected bool isAlive_ = false;

    public virtual void Init(UnitData unitData)
    {
        unitData_ = unitData;
        if (rigidbody_ == null)
        {
            rigidbody_ = GetComponent<Rigidbody>();
        }

        rigidbody_.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

        isAlive_ = true;
        SetUnitStatus(UNIT_STATUS.STAY);

        UnitPosition = new Vector3(UnitPosition.x, UnitDefine.mapUnitCreateYPosition_[unitData.UnitName], UnitPosition.z);
    }

    public virtual void Init(UnitData unitData, UnitAttackData unitAttackData)
    {
        Init(unitData);
    }

    public void SetDeadCallback(Action deadCallback)
    {
        deadCallback_ = deadCallback;
    }

    protected virtual void UnitUpdate()
    {
        unitHpGauge_.HpGaugeUpdate(unitData_.Hp, unitData_.MaxHp);
    }

    private void Update()
    {
        if (isAlive_ == false)
        {
            rigidbody_.velocity = Vector3.zero;
            return;
        }

        if(unitData_.StunTime > 0f)
        {
            unitData_.StunTime = unitData_.StunTime - Time.smoothDeltaTime;
        }
        else
        {
            unitData_.StunTime = 0f;
        }


        UnitUpdate();
    }

    protected virtual void Move(Vector3 targetUnitPosition)
    {
        rigidbody_.velocity = Vector3.zero;
        if (IsStun)
        {
            return;
        }

        if (unitData_.Speed <= 0f)
            return;

        SetUnitStatus(UNIT_STATUS.MOVE);

        Vector3 velo = Vector3.Normalize(targetUnitPosition - UnitPosition);
        velo.y = 0;
        velo *= unitData_.Speed;
        rigidbody_.velocity = velo;
    }

    protected void Stop(bool withMotionStay = false)
    {
        rigidbody_.velocity = Vector3.zero;

        if(withMotionStay)
        {
            SetUnitStatus(UNIT_STATUS.STAY);
        }
    }

    protected void UnitDestroy()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.DieSound);
        if (deadCallback_ != null)
        {
            deadCallback_.Invoke();
        }

        Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.UNIT_DEAD_EFFECT, this);

        isAlive_ = false;
        Hub_Ingame.Instance.unitController_.RestoreUnit(this);
    }

    public virtual void HitDmg(int damage, ELEMENT element)
    {
        if (isAlive_ == false)
            return;

        Vector3 hitPosition = UnitPosition + new Vector3(0, UnitDefine.mapUnitHitYPosition_[UnitName] * 2, 0);

        if(element == ELEMENT.NONE)
        {
            Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.HIT_NONE_ELEMENT, hitPosition);
            SoundManager.Instance.UseSoundEffect(SoundName.Hit_None);
        }
        else if (element == ELEMENT.FIRE)
        {
            Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.HIT_FIRE_ELEMENT, hitPosition);
            SoundManager.Instance.UseSoundEffect(SoundName.Hit_Magic);
        }
        else if (element == ELEMENT.ICE)
        {
            Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.HIT_ICE_ELEMENT, hitPosition);
            SoundManager.Instance.UseSoundEffect(SoundName.Hit_Magic);
        }
        else if (element == ELEMENT.AIR)
        {
            Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.HIT_AIR_ELEMENT, hitPosition);
            SoundManager.Instance.UseSoundEffect(SoundName.Hit_Magic);
        }
        else if (element == ELEMENT.ELECTRICITY)
        {
            Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.HIT_ELECTRICITY_ELEMENT, hitPosition);
            SoundManager.Instance.UseSoundEffect(SoundName.Hit_Magic);
        }

        Effect_Damage damageEffect = Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.DAMAGE_NUMBER, hitPosition) as Effect_Damage;

        if (damageEffect != null)
        {
            damageEffect.SetDamageText(damage);
        }

        unitData_.AddHp(-damage);
        if (unitData_.IsDead())
        {
            UnitDestroy();
        }
    }

    public virtual void HitDmg(int damage, ELEMENT element, float delayTime)
    {
        if (isAlive_ == false || gameObject.activeSelf == false)
            return;

        StartCoroutine(HitDmgCoroutine(damage, element, delayTime));
    }

    IEnumerator HitDmgCoroutine(int damage, ELEMENT element, float time)
    {
        yield return new WaitForSeconds(time);
        
        HitDmg(damage, element);
    }

    protected void SetUnitStatus(UNIT_STATUS status)
    {
        if (unitAnimator_ == null)
            return;

        unitStatus_ = status;

        if (status == UNIT_STATUS.STAY)
        {
            unitAnimator_.SetTrigger("Idle");
        }
        else if (status == UNIT_STATUS.MOVE)
        {
            unitAnimator_.SetTrigger("Move");
        }
        else if (status == UNIT_STATUS.ATTACK)
        {
            unitAnimator_.SetTrigger("Attack");
            //unitAnimator_.StartPlayback();
        }
        else if (status == UNIT_STATUS.DEAD)
        {
            unitAnimator_.SetTrigger("Dead");
        }
    }

    public void SetStunTime(float time)
    {
        unitData_.StunTime += time;
    }

    public void OnGameEnded()
    {
        isAlive_ = false;
    }

    public bool IsAlive
    {
        get { return isAlive_; }
    }

    public bool IsStun
    {
        get { return unitData_.StunTime > 0f; }
    }

    public UNIT_NAME UnitName
    {
        get { return unitData_.UnitName; }
    }

    public TEAM Team
    {
        get { return unitData_.Team; }
    }

    public CameraHolder CameraHolder
    {
        get { return cameraHolder_; }
    }

    public List<Renderer> TeamColorRenderers
    {
        get { return teamColorRenderer_; }
    }

    public Vector3 UnitPosition
    {
        get { return transform.localPosition; }
        set { transform.localPosition = value; }
    }

    public Vector3 WorldPosition
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public Rigidbody Rigidbody
    {
        get { return rigidbody_; }
    }

    public float HPRatio
    {
        get { return unitData_.Hp / unitData_.MaxHp; }
    }
}
