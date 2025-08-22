using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIController : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] GameObject unitCreatorUILayer_;
    [SerializeField] GameObject unitEnterUILayer_;
    [SerializeField] GameObject magicianLandingUILayer_;

    [Header("Select Checkers")]
    [SerializeField] GameObject unitSelectCheck_Soldier_; //유닛 UI 색상 변경
    [SerializeField] GameObject unitSelectCheckArcher_;
    [SerializeField] GameObject unitSelectCheckShielder_;
    [SerializeField] GameObject unitSelectCheckMagicianPlayer_;

    [SerializeField] GameObject unitEnterSelectChecker_;

    [Header("Unit Cost Texts")]
    [SerializeField] Text unitCostText_Soldier_;
    [SerializeField] Text unitCostText_Archer_;
    [SerializeField] Text unitCostText_Shielder_;

    [Header("Skill Icon Sprites")]
    [SerializeField] Sprite skillIcon_Fire_1_;
    [SerializeField] Sprite skillIcon_Fire_2_;

    [SerializeField] Sprite skillIcon_Ice_1_;
    [SerializeField] Sprite skillIcon_Ice_2_;

    [SerializeField] Sprite skillIcon_Air_1_;
    [SerializeField] Sprite skillIcon_Air_2_;

    [SerializeField] Sprite skillIcon_Electricity_1_;
    [SerializeField] Sprite skillIcon_Electricity_2_;

    [Header("Skill Icons")]
    [SerializeField] Transform skillGridTransform_;

    [SerializeField] GameObject skillIcon_Fire_;
    [SerializeField] GameObject skillIcon_Ice_;
    [SerializeField] GameObject skillIcon_Air_;
    [SerializeField] GameObject skillIcon_Electric_;

    [SerializeField] Image skillIconImage_Fire_;
    [SerializeField] Image skillIconImage_Ice_;
    [SerializeField] Image skillIconImage_Air_;
    [SerializeField] Image skillIconImage_Electricity_;

    [Header("Skill Cooltimes")]
    [SerializeField] Image enter_SkillGray_;
    [SerializeField] Image landing_SkillGray_;

    [SerializeField] Image fire_SkillGray_;
    [SerializeField] Image ice_SkillGray_;
    [SerializeField] Image air_SkillGray_;
    [SerializeField] Image electric_SkillGray_;

    [Header("Unit Status Bar")]
    [SerializeField] Image unitIconImage_Enter_;

    [SerializeField] Image unitHpGauge_Enter_;
    [SerializeField] Image unitLessTimeGauge_Enter_;

    [SerializeField] Image unitHpGauge_Landing_;
    [SerializeField] Image unitLessTimeGauge_Landing_;

    [Header("Unit Icon Sprites")]
    [SerializeField] Sprite unitIconSprite_MagicianPlayer_;
    [SerializeField] Sprite unitIconSprite_Base_;
    [SerializeField] Sprite unitIconSprite_Soldier_;
    [SerializeField] Sprite unitIconSprite_Archer_;
    [SerializeField] Sprite unitIconSprite_Shielder_;

    [Header("Common")]
    [SerializeField] Text costText_;
    //[SerializeField] Image costGauge_;


    Dictionary<UNIT_NAME, GameObject> mapSelectChecker_ = new Dictionary<UNIT_NAME, GameObject>();
    Dictionary<ELEMENT, GameObject> mapSkillIcon_ = new Dictionary<ELEMENT, GameObject>();
    Dictionary<ELEMENT, Image> mapSkillCooltime_ = new Dictionary<ELEMENT, Image>();

    float maxLessEnterTime_;
    float lessEnterTime_;

    private void Start()
    {
        OnCreatorPlayer();

        mapSelectChecker_.Clear();
        mapSelectChecker_.Add(UNIT_NAME.SOLDIER, unitSelectCheck_Soldier_);
        mapSelectChecker_.Add(UNIT_NAME.ARCHER, unitSelectCheckArcher_);
        mapSelectChecker_.Add(UNIT_NAME.SHIELDER, unitSelectCheckShielder_);
        mapSelectChecker_.Add(UNIT_NAME.MAGICIAN_PLAYER, unitSelectCheckMagicianPlayer_);

        mapSkillIcon_.Clear();
        mapSkillIcon_.Add(ELEMENT.FIRE, skillIcon_Fire_);
        mapSkillIcon_.Add(ELEMENT.ICE, skillIcon_Ice_);
        mapSkillIcon_.Add(ELEMENT.AIR, skillIcon_Air_);
        mapSkillIcon_.Add(ELEMENT.ELECTRICITY, skillIcon_Electric_);

        mapSkillCooltime_.Clear();
        mapSkillCooltime_.Add(ELEMENT.FIRE, fire_SkillGray_);
        mapSkillCooltime_.Add(ELEMENT.ICE, ice_SkillGray_);
        mapSkillCooltime_.Add(ELEMENT.AIR, air_SkillGray_);
        mapSkillCooltime_.Add(ELEMENT.ELECTRICITY, electric_SkillGray_);

        unitCostText_Soldier_.text = UnitDefine.mapUnitCost_[UNIT_NAME.SOLDIER].ToString();
        unitCostText_Archer_.text = UnitDefine.mapUnitCost_[UNIT_NAME.ARCHER].ToString();
        unitCostText_Shielder_.text = UnitDefine.mapUnitCost_[UNIT_NAME.SHIELDER].ToString();

        SkillIconInit();
    }

    void SkillIconInit()
    {
        SKILL_NAME fire_SkillName = Context.g_MapSelectedSkillByElement_[ELEMENT.FIRE];
        SKILL_NAME ice_SkillName = Context.g_MapSelectedSkillByElement_[ELEMENT.ICE];
        SKILL_NAME air_SkillName = Context.g_MapSelectedSkillByElement_[ELEMENT.AIR];
        SKILL_NAME electricity_SkillName = Context.g_MapSelectedSkillByElement_[ELEMENT.ELECTRICITY];

        if (fire_SkillName == SKILL_NAME.FIRE_FIRERAIN)
        {
            skillIconImage_Fire_.sprite = skillIcon_Fire_1_;
        }
        else if (fire_SkillName == SKILL_NAME.FIRE_METEOR)
        {
            skillIconImage_Fire_.sprite = skillIcon_Fire_2_;
        }

        if (ice_SkillName == SKILL_NAME.ICE_BLIZZARD)
        {
            skillIconImage_Ice_.sprite = skillIcon_Ice_1_;
        }
        else if (ice_SkillName == SKILL_NAME.ICE_GLACIALUPRISE)
        {
            skillIconImage_Ice_.sprite = skillIcon_Ice_2_;
        }

        if (air_SkillName == SKILL_NAME.AIR_AEROVORTEX)
        {
            skillIconImage_Air_.sprite = skillIcon_Air_1_;
        }
        else if (air_SkillName == SKILL_NAME.AIR_TEMPESTBLAST)
        {
            skillIconImage_Air_.sprite = skillIcon_Air_2_;
        }

        if (electricity_SkillName == SKILL_NAME.ELEC_CHAINLIGHTNING)
        {
            skillIconImage_Electricity_.sprite = skillIcon_Electricity_1_;
        }
        else if (electricity_SkillName == SKILL_NAME.ELEC_THUNDERRESIDUE)
        {
            skillIconImage_Electricity_.sprite = skillIcon_Electricity_2_;
        }
    }

    private void Update()
    {
        if (lessEnterTime_ >= 0f) 
        {
            lessEnterTime_ -= Time.smoothDeltaTime;
        }
        else
        {
            lessEnterTime_ = 0f;
        }

        unitLessTimeGauge_Enter_.fillAmount = lessEnterTime_ / maxLessEnterTime_;
        unitLessTimeGauge_Landing_.fillAmount = lessEnterTime_ / maxLessEnterTime_;
    }

    public void OnCreatorPlayer()
    {
        unitCreatorUILayer_.SetActive(true);
        unitEnterUILayer_.SetActive(false);
        magicianLandingUILayer_.SetActive(false);
    }

    public void OnEnterUnit(UNIT_NAME unitName, ELEMENT useableElement, float enterTime)
    {
        skillGridTransform_.parent = unitEnterUILayer_.transform;

        unitCreatorUILayer_.SetActive(false);
        unitEnterUILayer_.SetActive(true);
        magicianLandingUILayer_.SetActive(false);

        mapSkillIcon_[ELEMENT.FIRE].SetActive(useableElement == ELEMENT.FIRE);
        mapSkillIcon_[ELEMENT.ICE].SetActive(useableElement == ELEMENT.ICE);
        mapSkillIcon_[ELEMENT.AIR].SetActive(useableElement == ELEMENT.AIR);
        mapSkillIcon_[ELEMENT.ELECTRICITY].SetActive(useableElement == ELEMENT.ELECTRICITY);

        OnEnterUnit(enterTime);

        Sprite unitIconSprite = unitIconSprite_Base_;
        
        if(unitName == UNIT_NAME.SOLDIER)
        {
            unitIconSprite = unitIconSprite_Soldier_;
        }
        else if (unitName == UNIT_NAME.ARCHER)
        {
            unitIconSprite = unitIconSprite_Archer_;
        }
        else if (unitName == UNIT_NAME.SHIELDER)
        {
            unitIconSprite = unitIconSprite_Shielder_;
        }

        unitIconImage_Enter_.sprite = unitIconSprite;
    }

    public void OnLandingMagician(float enterTime)
    {
        skillGridTransform_.parent = magicianLandingUILayer_.transform;

        unitCreatorUILayer_.SetActive(false);
        unitEnterUILayer_.SetActive(false);
        magicianLandingUILayer_.SetActive(true);

        mapSkillIcon_[ELEMENT.FIRE].SetActive(true);
        mapSkillIcon_[ELEMENT.ICE].SetActive(true);
        mapSkillIcon_[ELEMENT.AIR].SetActive(true);
        mapSkillIcon_[ELEMENT.ELECTRICITY].SetActive(true);

        OnEnterUnit(enterTime);
    }

    public void SetEnterCooltime(float ratio)
    {
        enter_SkillGray_.fillAmount = 1f - ratio;
    }

    public void SetLandingCooltime(float ratio)
    {
        landing_SkillGray_.fillAmount = 1f - ratio;
    }

    public void SetUnitHpBar(float ratio)
    {
        unitHpGauge_Enter_.fillAmount = ratio;
        unitHpGauge_Landing_.fillAmount = ratio;
    }

    public void SetSkillCooltime(ELEMENT element, float ratio)
    {
        if (mapSkillCooltime_.ContainsKey(element) == false)
            return;

        mapSkillCooltime_[element].fillAmount = 1f - ratio;
    }

    public void OnKeyDownCreateUnitBtn(UNIT_NAME unitName, bool isActive)
    {
        if (mapSelectChecker_.ContainsKey(unitName) == false)
            return;

        mapSelectChecker_[unitName].SetActive(isActive);
    }

    public void OnKeyDownEnterUnitBtn(bool isActive)
    {
        unitEnterSelectChecker_.SetActive(isActive);
    }

    void OnEnterUnit(float enterTime)
    {
        maxLessEnterTime_ = enterTime;
        lessEnterTime_ = maxLessEnterTime_;
    }

    public void OnCostUpdate(int curCost)
    {
        costText_.text = curCost.ToString();
        //costGauge_.fillAmount = (float)curCost / UnitDefine.MAX_COST;
    }
}
