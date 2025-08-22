using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_CreatorPlayer : Unit_Playable
{
    [Header("HologramUnits")]
    [SerializeField] UnitHologram hologram_Soldier_;
    [SerializeField] UnitHologram hologram_Archer_;
    [SerializeField] UnitHologram hologram_Shielder_;
    [SerializeField] UnitHologram hologram_MagicianPlayer_;

    Dictionary<UNIT_NAME, UnitHologram> mapHologramUnit_ = new Dictionary<UNIT_NAME, UnitHologram>();

    [Header("Common")]
    [SerializeField] LineRenderer aimLineRenderer_;
    [SerializeField] UnitSkillController unitSkillControllerOrigin_;

    Stage curStage_;

    List<TEAM> enemyTeamList_ = new List<TEAM>();
    UNIT_NAME selectCreateReadyUnitName_ = UNIT_NAME.NONE;
    bool isSelectEnterMode_;

    bool isMindControlling_;
    Unit enterUnit_;

    int cost_;
    int Cost
    {
        get { return cost_; }
        set
        {
            cost_ = value;
            Hub_Ingame.Instance.stageUIController_.OnCostUpdate(cost_);
        }
    }
    float costChargeTime_ = 0.5f;
    float elapsedTime_;

    const float magicianLandingDelayTime_ = 55f;
    float magicianLandingCooltime_ = magicianLandingDelayTime_;

    const float unitEnterDelayTime_ = 30f;
    float unitEnterCooltime_ = unitEnterDelayTime_;


    public override void Init(UnitData unitData)
    {
        base.Init(unitData);

        isCanFly_ = true;
        isSelectEnterMode_ = false;
        isMindControlling_ = false;

        HologramUnitInit();

        speed_ = 1.5f;

        Cost = 0;

        enemyTeamList_.Clear();
        enemyTeamList_.Add(TEAM.TEAM_2);
        
        unitSkillControllerOrigin_.Init(this, null, enemyTeamList_);
        curStage_ = Hub_Ingame.Instance.stageController_.CurrentField.CurStage;
    }

    void HologramUnitInit()
    {
        mapHologramUnit_.Add(UNIT_NAME.NONE, null);
        mapHologramUnit_.Add(UNIT_NAME.SOLDIER, hologram_Soldier_);
        mapHologramUnit_.Add(UNIT_NAME.ARCHER, hologram_Archer_);
        mapHologramUnit_.Add(UNIT_NAME.SHIELDER, hologram_Shielder_);
        mapHologramUnit_.Add(UNIT_NAME.MAGICIAN_PLAYER, hologram_MagicianPlayer_);
    }

    protected override void PlayableUnitUpdate()
    {
        elapsedTime_ += Time.smoothDeltaTime;
        if (elapsedTime_ >= costChargeTime_) 
        {
            if(Cost < UnitDefine.MAX_COST)
            {
                Cost += 1;
            }
            elapsedTime_ -= costChargeTime_;
        }

        magicianLandingCooltime_ += Time.smoothDeltaTime;
        unitEnterCooltime_ += Time.smoothDeltaTime;
        Hub_Ingame.Instance.stageUIController_.SetLandingCooltime(magicianLandingCooltime_ / magicianLandingDelayTime_);
        Hub_Ingame.Instance.stageUIController_.SetEnterCooltime(unitEnterCooltime_ / unitEnterDelayTime_);

        if (Context.g_IsPracticeMode_)
        {
            Cost = 100;
            magicianLandingCooltime_ = 999f;
            unitEnterCooltime_ = 999f;
        }

        if (isMindControlling_)
        {
            Hub_Ingame.Instance.stageUIController_.SetUnitHpBar(enterUnit_.HPRatio);

            return;
        }


        base.PlayableUnitUpdate();
        
        // Enter Camera Mode Input
        if(Input.GetKeyDown(KeyCode.T))
        {
            isSelectEnterMode_ = !isSelectEnterMode_;
            Hub_Ingame.Instance.stageUIController_.OnKeyDownEnterUnitBtn(isSelectEnterMode_);

            aimLineRenderer_.gameObject.SetActive(isSelectEnterMode_);
        }

        // Unit Create Input
        if(Input.GetKeyDown(KeyCode.V))
        {
            OnKeydownCreateUnitBtn(UNIT_NAME.MAGICIAN_PLAYER);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnKeydownCreateUnitBtn(UNIT_NAME.SOLDIER);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnKeydownCreateUnitBtn(UNIT_NAME.ARCHER);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnKeydownCreateUnitBtn(UNIT_NAME.SHIELDER);
        }

        Vector3 fwd = CameraHolder.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(CameraHolder.transform.position, fwd * 100f, Color.blue); //

        if (selectCreateReadyUnitName_ != UNIT_NAME.NONE)
        {
            UnitHologram hologram = mapHologramUnit_[selectCreateReadyUnitName_];

            // CreateUnit MouseRaycast
            RaycastHit hit;
            int layerMask = (1 << LayerMask.NameToLayer("Ground"));
            bool isHitRay = Physics.Raycast(CameraHolder.transform.position, fwd, out hit, 100f, layerMask);
            if (isHitRay)
            {
                hologram.isNotOnTheGround = false;
                hologram.transform.position = hit.point + new Vector3(0, 0.05f, 0);
                hologram.transform.eulerAngles = Vector3.zero;

                if (Input.GetMouseButtonDown(0))
                {
                    if (IsCreateableUnit())
                    {
                        CreateUnit(selectCreateReadyUnitName_, hit.point);
                    }
                }
            }
            else
            {
                hologram.isNotOnTheGround = true;
            }
        }

        if (isSelectEnterMode_ == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                int layerMask = (1 << LayerMask.NameToLayer("Unit"));
                bool isHitRay = Physics.Raycast(CameraHolder.transform.position, fwd, out hit, 100f, layerMask);
                if (isHitRay)
                {
                    UnitCollider unitCollider = hit.collider.gameObject.GetComponent<UnitCollider>();

                    if (unitCollider != null)
                    {
                        Unit targetUnit = unitCollider.OwnUnit;

                        OnEnterUnit(targetUnit, UnitDefine.ENTER_UNIT_TIME);
                        Hub_Ingame.Instance.stageUIController_.OnEnterUnit(targetUnit.UnitName, Context.selectElement_, UnitDefine.ENTER_UNIT_TIME); //
                        unitEnterCooltime_ = 0f;
                    }
                }
            }
        }
    }

    void OnKeydownCreateUnitBtn(UNIT_NAME unitName)
    {
        SetAimLineRendererActive(false);
        Hub_Ingame.Instance.stageUIController_.OnKeyDownCreateUnitBtn(selectCreateReadyUnitName_, false);
        mapHologramUnit_[selectCreateReadyUnitName_]?.SetHologramDisable();

        selectCreateReadyUnitName_ = selectCreateReadyUnitName_ == unitName ? UNIT_NAME.NONE : unitName;

        if (selectCreateReadyUnitName_ != UNIT_NAME.NONE)
        {
            SetAimLineRendererActive(true);
            Hub_Ingame.Instance.stageUIController_.OnKeyDownCreateUnitBtn(selectCreateReadyUnitName_, true);
            mapHologramUnit_[selectCreateReadyUnitName_]?.SetHologramEnable();
        }
    }

    bool IsCreateableUnit()
    {
        if(mapHologramUnit_[selectCreateReadyUnitName_].isCollisionUnit)
            return false;

        if (Cost < UnitDefine.mapUnitCost_[selectCreateReadyUnitName_])
            return false;

        if (selectCreateReadyUnitName_ == UNIT_NAME.MAGICIAN_PLAYER &&
            magicianLandingCooltime_ < magicianLandingDelayTime_)
            return false;

        return true;
    }

    void CreateUnit(UNIT_NAME unitName, Vector3 makePosition)
    {
        if(unitName == UNIT_NAME.MAGICIAN_PLAYER)
        {
            UnitData unitData = new UnitData(unitName, 5f, 80, Team);
                
            Unit magicianPlayerUnit = Hub_Ingame.Instance.unitController_.UseUnit(unitData, makePosition);
            SetAimLineRendererActive(false);
            Hub_Ingame.Instance.stageUIController_.OnKeyDownCreateUnitBtn(selectCreateReadyUnitName_, false);
            mapHologramUnit_[selectCreateReadyUnitName_]?.SetHologramDisable();

            OnEnterUnit(magicianPlayerUnit, UnitDefine.MAGICIAN_LANDING_TIME);
            Hub_Ingame.Instance.stageUIController_.OnLandingMagician(UnitDefine.MAGICIAN_LANDING_TIME);
            magicianLandingCooltime_ = 0f;
        }
        else
        {
            UnitData unitData = new UnitData(unitName, 3.0f, 40, Team);
            UnitAttackData unitAttackData = new UnitAttackData(5, 2f, 1.1f);

            if (unitName == UNIT_NAME.SOLDIER)
            {
                unitData = new UnitData(unitName, 2.5f, 130 + (10 * curStage_.stage_), Team);
                unitAttackData = new UnitAttackData(20 + (10 * curStage_.stage_), 2f, 2f);
            }
            else if (unitName == UNIT_NAME.ARCHER)
            {
                unitData = new UnitData(unitName, 2.0f, 80 + (10 * curStage_.stage_), Team);
                unitAttackData = new UnitAttackData(30 + (10 * curStage_.stage_), 9f, 2.5f);
            }
            else if (unitName == UNIT_NAME.SHIELDER)
            {
                unitData = new UnitData(unitName, 3.0f, 200 + (10 * curStage_.stage_), Team);
                unitAttackData = new UnitAttackData(10 + (10 * curStage_.stage_), 1.5f, 3.0f);
            }

            Hub_Ingame.Instance.unitController_.UseUnit(unitData, unitAttackData, makePosition);
            SetAimLineRendererActive(false);
            Hub_Ingame.Instance.stageUIController_.OnKeyDownCreateUnitBtn(selectCreateReadyUnitName_, false);
            mapHologramUnit_[selectCreateReadyUnitName_]?.SetHologramDisable();
        }

        Cost = Cost - UnitDefine.mapUnitCost_[unitName];
        selectCreateReadyUnitName_ = UNIT_NAME.NONE;
    }

    void OnEnterUnit(Unit targetUnit, float enterTime)
    {
        if (targetUnit.Team == Team)
        {
            enterUnit_ = targetUnit;
            isMindControlling_ = true;
            SetUnitSkillController(targetUnit);

            Hub_Ingame.Instance.cameraController_.CameraEnterTeamUnit(targetUnit, enterTime,
                delegate ()
                {
                    EnterCameraEndCallback();
                    if(targetUnit.UnitName == UNIT_NAME.MAGICIAN_PLAYER)
                    {
                        Hub_Ingame.Instance.unitController_.RestoreUnit(targetUnit);
                    }
                });
        }
    }

    void EnterCameraEndCallback()
    {
        Hub_Ingame.Instance.stageUIController_.OnCreatorPlayer();

        selectCreateReadyUnitName_ = UNIT_NAME.NONE;
        isSelectEnterMode_ = false;
        isMindControlling_ = false;

        Hub_Ingame.Instance.stageUIController_.OnKeyDownEnterUnitBtn(isSelectEnterMode_);
        SetAimLineRendererActive(false);
        SetUnitSkillController(this);
        unitSkillControllerOrigin_.Init(this, null, enemyTeamList_);

        Hub_Ingame.Instance.cameraController_.SetCameraHolding(CameraHolder);
    }

    void SetAimLineRendererActive(bool value)
    {
        aimLineRenderer_.gameObject.SetActive(value);
        Hub_Ingame.Instance.stageController_.CurrentField.Team_1_Createable_Ground_Active = value;
    }

    public void SetUnitSkillController(Unit targetUnit)
    {
        List<ELEMENT> elementList = new List<ELEMENT>();

        if (targetUnit.UnitName == UNIT_NAME.MAGICIAN_PLAYER)
        {
            elementList.Add(ELEMENT.FIRE);
            elementList.Add(ELEMENT.ICE);
            elementList.Add(ELEMENT.AIR);
            elementList.Add(ELEMENT.ELECTRICITY);
            unitSkillControllerOrigin_.Init(targetUnit, elementList, enemyTeamList_);
        }
        else
        {
            elementList.Add(Context.selectElement_);
            unitSkillControllerOrigin_.Init(targetUnit, elementList, enemyTeamList_);
        }
    }
}
