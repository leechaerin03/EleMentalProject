using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventoryBox : MonoBehaviour
{
    [SerializeField] GameObject isSelectChecker_;
    [SerializeField] Image skillIconImage_;

    [Header("Skill Icon Sprites")]
    [SerializeField] Sprite skillIcon_Fire_1_;
    [SerializeField] Sprite skillIcon_Fire_2_;

    [SerializeField] Sprite skillIcon_Ice_1_;
    [SerializeField] Sprite skillIcon_Ice_2_;

    [SerializeField] Sprite skillIcon_Air_1_;
    [SerializeField] Sprite skillIcon_Air_2_;

    [SerializeField] Sprite skillIcon_Electricity_1_;
    [SerializeField] Sprite skillIcon_Electricity_2_;

    ELEMENT element_;
    SKILL_NAME skillName_;

    Action onClickCallback_ = null;

    public void Init(ELEMENT element, SKILL_NAME skillName, Action onClickCallback)
    {
        element_ = element;
        skillName_ = skillName;

        if(element == ELEMENT.FIRE)
        {
            if (skillName_ == SKILL_NAME.FIRE_FIRERAIN)
            {
                skillIconImage_.sprite = skillIcon_Fire_1_;
            }
            else if (skillName_ == SKILL_NAME.FIRE_METEOR)
            {
                skillIconImage_.sprite = skillIcon_Fire_2_;
            }
        }
        else if (element == ELEMENT.ICE)
        {
            if (skillName_ == SKILL_NAME.ICE_BLIZZARD)
            {
                skillIconImage_.sprite = skillIcon_Ice_1_;
            }
            else if (skillName_ == SKILL_NAME.ICE_GLACIALUPRISE)
            {
                skillIconImage_.sprite = skillIcon_Ice_2_;
            }
        }
        else if (element == ELEMENT.AIR)
        {
            if (skillName_ == SKILL_NAME.AIR_AEROVORTEX)
            {
                skillIconImage_.sprite = skillIcon_Air_1_;
            }
            else if (skillName_ == SKILL_NAME.AIR_TEMPESTBLAST)
            {
                skillIconImage_.sprite = skillIcon_Air_2_;
            }
        }
        else if (element == ELEMENT.ELECTRICITY)
        {
            if (skillName_ == SKILL_NAME.ELEC_CHAINLIGHTNING)
            {
                skillIconImage_.sprite = skillIcon_Electricity_1_;
            }
            else if (skillName_ == SKILL_NAME.ELEC_THUNDERRESIDUE)
            {
                skillIconImage_.sprite = skillIcon_Electricity_2_;
            }
        }


        isSelectChecker_.SetActive(false);

        if (Context.g_MapSelectedSkillByElement_[element] == skillName)
        {
            isSelectChecker_.SetActive(true);
        }

        onClickCallback_ = onClickCallback;
    }

    public void OnClickThisSkillIcon()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        Context.OnClickSelectUsingSkill(element_, skillName_);

        onClickCallback_?.Invoke();
    }
}
