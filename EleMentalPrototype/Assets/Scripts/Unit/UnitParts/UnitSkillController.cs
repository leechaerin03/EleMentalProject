using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitSkillController : MonoBehaviour
{
    [Header("HologramSkills")]
    [SerializeField] SkillHologram hologram_AeroVortex_;
    [SerializeField] SkillHologram hologram_TempestBlast_;
    [SerializeField] SkillHologram hologram_ThunderResidue_;
    [SerializeField] SkillHologram hologram_Meteor_;
    [SerializeField] SkillHologram hologram_Blizzard_;
    [SerializeField] SkillHologram hologram_GlacialUprise_;

    Dictionary<SKILL_NAME, SkillHologram> mapHologramSkill_ = new Dictionary<SKILL_NAME, SkillHologram>();

    [Header("Common")]
    [SerializeField] LineRenderer aimLineRenderer_;

    Unit curUnit_;
    ELEMENT selectedUsingElement_;

    Dictionary<ELEMENT, float> mapSkillCooltime_ = new Dictionary<ELEMENT, float>();
    List<ELEMENT> useableElementList_ = new List<ELEMENT>();
    List<TEAM> enemyTeamList_ = new List<TEAM>();
    Dictionary<ELEMENT, SKILL_NAME> mapEquipSkill_ = new Dictionary<ELEMENT, SKILL_NAME>();


    public void Init(Unit useUnit, List<ELEMENT> useableElementList = null, List<TEAM> enemyTeamList = null)
    {
        curUnit_ = useUnit;
        if(useableElementList != null)
        {
            useableElementList_ = useableElementList;
        }
        else
        {
            useableElementList_ = new List<ELEMENT>();
        }
        if (enemyTeamList != null)
        {
            enemyTeamList_ = enemyTeamList;
        }
        else
        {
            enemyTeamList_ = new List<TEAM>();
        }

        transform.parent = useUnit.CameraHolder.transform;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = new Vector3(1, 1, 1);

        HologramSkillInit();

        selectedUsingElement_ = ELEMENT.NONE;
        
        mapEquipSkill_.Clear();
        mapEquipSkill_.Add(ELEMENT.NONE, SKILL_NAME.NONE);
        mapEquipSkill_.Add(ELEMENT.FIRE, Context.g_MapSelectedSkillByElement_[ELEMENT.FIRE]);
        mapEquipSkill_.Add(ELEMENT.ICE, Context.g_MapSelectedSkillByElement_[ELEMENT.ICE]);
        mapEquipSkill_.Add(ELEMENT.AIR, Context.g_MapSelectedSkillByElement_[ELEMENT.AIR]);
        mapEquipSkill_.Add(ELEMENT.ELECTRICITY, Context.g_MapSelectedSkillByElement_[ELEMENT.ELECTRICITY]);

        mapSkillCooltime_.Clear();
        mapSkillCooltime_.Add(ELEMENT.FIRE, 999f);
        mapSkillCooltime_.Add(ELEMENT.ICE, 999f);
        mapSkillCooltime_.Add(ELEMENT.AIR, 999f);
        mapSkillCooltime_.Add(ELEMENT.ELECTRICITY, 999f);
    }

    void HologramSkillInit()
    {
        mapHologramSkill_.Clear();

        mapHologramSkill_.Add(SKILL_NAME.NONE, null);
        mapHologramSkill_.Add(SKILL_NAME.AIR_AEROVORTEX, hologram_AeroVortex_);
        mapHologramSkill_.Add(SKILL_NAME.AIR_TEMPESTBLAST, hologram_TempestBlast_);
        mapHologramSkill_.Add(SKILL_NAME.ELEC_THUNDERRESIDUE, hologram_ThunderResidue_);
        mapHologramSkill_.Add(SKILL_NAME.FIRE_METEOR, hologram_Meteor_);
        mapHologramSkill_.Add(SKILL_NAME.ICE_BLIZZARD, hologram_Blizzard_);
        mapHologramSkill_.Add(SKILL_NAME.ICE_GLACIALUPRISE, hologram_GlacialUprise_);

        List<SKILL_NAME> skillNameArr = Enum.GetValues(typeof(SKILL_NAME)).Cast<SKILL_NAME>().ToList();
        for (int i = 0; i < skillNameArr.Count; ++i)
        {
            if (mapHologramSkill_.ContainsKey(skillNameArr[i]) == false)
                continue;

            if (skillNameArr[i] == SKILL_NAME.NONE)
                continue;

            mapHologramSkill_[skillNameArr[i]].gameObject.SetActive(false);
        }

        aimLineRenderer_.gameObject.SetActive(false);
    }

    private void Update()
    {
        for(int i = 0; i < useableElementList_.Count; ++i)
        {
            ELEMENT element = useableElementList_[i];

            SKILL_NAME skillName = mapEquipSkill_[element];
            mapSkillCooltime_[element] += Time.smoothDeltaTime;
            Hub_Ingame.Instance.stageUIController_.SetSkillCooltime(element, mapSkillCooltime_[element] / SkillDefine.mapSkillCooltime_[skillName]);

            if (Context.g_IsPracticeMode_)
            {
                mapSkillCooltime_[element] += 999f;
            }
        }

        if(useableElementList_.Count <= 0)
            return;

        Vector3 fwd = curUnit_.CameraHolder.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(curUnit_.CameraHolder.transform.position, fwd * 100f, Color.blue); //

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnKeydownUsingSkill(ELEMENT.FIRE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnKeydownUsingSkill(ELEMENT.ICE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnKeydownUsingSkill(ELEMENT.AIR);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OnKeydownUsingSkill(ELEMENT.ELECTRICITY);
        }

        if (selectedUsingElement_ != ELEMENT.NONE)
        {
            SKILL_NAME skillName = mapEquipSkill_[selectedUsingElement_];

            if (mapHologramSkill_.ContainsKey(skillName) == false)
                return;

            SkillHologram hologram = mapHologramSkill_[skillName];

            // CreateUnit MouseRaycast
            RaycastHit hit;
            int layerMask = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("CanSkillGround"));
            bool isHitRay = Physics.Raycast(curUnit_.CameraHolder.transform.position, fwd, out hit, 100f, layerMask);
            if (isHitRay)
            {
                hologram.isNotOnTheGround = false;
                hologram.transform.position = hit.point;
                hologram.transform.eulerAngles = Vector3.zero;

                if (Input.GetMouseButtonDown(0))
                {
                    if (IsSkillUseable(selectedUsingElement_))
                    {
                        CreateSkill(skillName, hit.point);
                    }
                }
            }
            else
            {
                hologram.isNotOnTheGround = true;
            }

            hologram.transform.localPosition = new Vector3(hologram.transform.localPosition.x, 0.05f, hologram.transform.localPosition.z);
        }

    }

    void OnKeydownUsingSkill(ELEMENT element)
    {
        if (useableElementList_.Contains(element) == false)
            return;

        SKILL_NAME skillName = mapEquipSkill_[element];
        SKILL_NAME beforeSkillName = mapEquipSkill_[selectedUsingElement_];

        aimLineRenderer_.gameObject.SetActive(false);
        // Hub_Ingame.Instance.stageUIController_.OnKeyDownCreateUnitBtn(selectedUsingSkill_, false);
        mapHologramSkill_[beforeSkillName]?.SetHologramDisable();

        selectedUsingElement_ = selectedUsingElement_ == element ? ELEMENT.NONE : element;

        if (selectedUsingElement_ != ELEMENT.NONE)
        {
            if (SkillDefine.mapSkillUsingType_[skillName] == SKILL_USE_TYPE.IMMEDIATE)
            {
                if (IsSkillUseable(element))
                {
                    CreateSkill(skillName, curUnit_.UnitPosition);
                }

                selectedUsingElement_ = ELEMENT.NONE;
                return;
            }

            aimLineRenderer_.gameObject.SetActive(true);
            //Hub_Ingame.Instance.stageUIController_.OnKeyDownCreateUnitBtn(selectedUsingSkill_, true);
            mapHologramSkill_[skillName]?.SetHologramEnable();
        }
    }

    bool IsSkillUseable(ELEMENT element)
    {
        if (mapSkillCooltime_[element] < SkillDefine.mapSkillCooltime_[mapEquipSkill_[element]])
            return false;
        
        return true;
    }

    void CreateSkill(SKILL_NAME skillName, Vector3 makePosition)
    {
        // AimRender And Hologram
        aimLineRenderer_.gameObject.SetActive(false);
        if (mapHologramSkill_.ContainsKey(skillName))
        {
            mapHologramSkill_[skillName]?.SetHologramDisable();
        }

        //Hub_Ingame.Instance.stageUIController_.OnKeyDownCreateUnitBtn(selectedUsingSkill_, false);
        
        if (skillName == SKILL_NAME.AIR_AEROVORTEX)
        {
            // Skill Create
            Skill skill = Hub_Ingame.Instance.skillController_.UseSkill(skillName, makePosition, 75, enemyTeamList_);

            // Skill Component Create
            SkillComp_PushOut skillComp_PushOut = new SkillComp_PushOut();
            skillComp_PushOut.Init(skill.SkillPosition, -1000f);
            skill.AddSkillComponent(skillComp_PushOut);

            // Effect Create
            Effect effect = Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.CIRCLE_REFRECTION, makePosition);

            SoundManager.Instance.UseSoundEffect(SoundName.AeroVortex);
        }
        else if (skillName == SKILL_NAME.AIR_TEMPESTBLAST)
        {
            // Skill Create
            Skill skill = Hub_Ingame.Instance.skillController_.UseSkill(skillName, makePosition, 150, enemyTeamList_);

            // Skill Component Create
            SkillComp_PushOut skillComp_PushOut = new SkillComp_PushOut();
            skillComp_PushOut.Init(skill.SkillPosition, 1000f);
            skill.AddSkillComponent(skillComp_PushOut);

            // Effect Create
            Effect effect = Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.CIRCLE_REFRECTION_FROM_CAMERA);
            effect.transform.parent = curUnit_.CameraHolder.transform;
            effect.transform.localPosition = Vector3.zero;
            effect.transform.localEulerAngles = Vector3.zero;

            SoundManager.Instance.UseSoundEffect(SoundName.TempestBlast);
        }
        else if (skillName == SKILL_NAME.ELEC_CHAINLIGHTNING)
        {
            // Skill Create
            Skill skill = Hub_Ingame.Instance.skillController_.UseSkill(skillName, makePosition + curUnit_.CameraHolder.transform.forward * 3f, 6, enemyTeamList_);

            // Skill Component Create
            SkillComp_Stun skillComp_Stun = new SkillComp_Stun();
            skillComp_Stun.Init(0.5f);
            skill.AddSkillComponent(skillComp_Stun);

            SoundManager.Instance.UseSoundEffect(SoundName.ChainLightning);
        }
        else if (skillName == SKILL_NAME.ELEC_THUNDERRESIDUE)
        {
            // Skill Create
            Skill skill = Hub_Ingame.Instance.skillController_.UseSkill(skillName, makePosition, 40, enemyTeamList_, 3);

            // Skill Component Create
            SkillComp_Stun skillComp_Stun = new SkillComp_Stun();
            skillComp_Stun.Init(0.5f);
            skill.AddSkillComponent(skillComp_Stun);

            // Effect Create

            SoundManager.Instance.UseSoundEffect(SoundName.ThunderResidue);
        }
        else if (skillName == SKILL_NAME.FIRE_FIRERAIN)
        {
            // Skill Create
            Skill skill = Hub_Ingame.Instance.skillController_.UseSkill(skillName, makePosition + curUnit_.CameraHolder.transform.forward * -2f, 8, enemyTeamList_);


            SoundManager.Instance.UseSoundEffect(SoundName.FireRain);
        }
        else if (skillName == SKILL_NAME.FIRE_METEOR)
        {
            // Skill Create
            Skill skill = Hub_Ingame.Instance.skillController_.UseSkill(skillName, makePosition, 80, enemyTeamList_);

            // Skill Component Create

            // Effect Create
            Effect effect = Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.CIRCLE_REFRECTION, makePosition);

            SoundManager.Instance.UseSoundEffect(SoundName.Meteor);
        }
        else if (skillName == SKILL_NAME.ICE_BLIZZARD)
        {
            // Skill Create
            Skill skill = Hub_Ingame.Instance.skillController_.UseSkill(skillName, makePosition, 15, enemyTeamList_, 10);

            // Skill Component Create
            SkillComp_Stun skillComp_Stun = new SkillComp_Stun();
            skillComp_Stun.Init(0.2f);
            skill.AddSkillComponent(skillComp_Stun);

            // Effect Create

            SoundManager.Instance.UseSoundEffect(SoundName.Blizzard);
        }
        else if (skillName == SKILL_NAME.ICE_GLACIALUPRISE)
        {
            // Skill Create
            Skill skill = Hub_Ingame.Instance.skillController_.UseSkill(skillName, makePosition, 180, enemyTeamList_);

            // Skill Component Create
            SkillComp_Stun skillComp_Stun = new SkillComp_Stun();
            skillComp_Stun.Init(2f);
            skill.AddSkillComponent(skillComp_Stun);

            // Effect Create
            Effect effect = Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.ICE_CRISTAL, makePosition);
            Debug.Log(skillName);


            SoundManager.Instance.UseSoundEffect(SoundName.GlacialUprise);
        }

        mapSkillCooltime_[selectedUsingElement_] = 0;
        selectedUsingElement_ = ELEMENT.NONE;
    }
}
