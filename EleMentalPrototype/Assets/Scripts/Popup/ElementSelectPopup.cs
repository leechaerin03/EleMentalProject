using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElementSelectPopup : Popup
{
    [SerializeField] Image skillIcon_Fire_;
    [SerializeField] Image skillIcon_Ice_;
    [SerializeField] Image skillIcon_Air_;
    [SerializeField] Image skillIcon_Electricity_;

    [Header("Skill Icon Sprites")]
    [SerializeField] Sprite skillIcon_Fire_1_;
    [SerializeField] Sprite skillIcon_Fire_2_;

    [SerializeField] Sprite skillIcon_Ice_1_;
    [SerializeField] Sprite skillIcon_Ice_2_;

    [SerializeField] Sprite skillIcon_Air_1_;
    [SerializeField] Sprite skillIcon_Air_2_;

    [SerializeField] Sprite skillIcon_Electricity_1_;
    [SerializeField] Sprite skillIcon_Electricity_2_;

    public override void Init()
    {
        base.Init();

        SKILL_NAME fire_SkillName = Context.g_MapSelectedSkillByElement_[ELEMENT.FIRE];
        SKILL_NAME ice_SkillName = Context.g_MapSelectedSkillByElement_[ELEMENT.ICE];
        SKILL_NAME air_SkillName = Context.g_MapSelectedSkillByElement_[ELEMENT.AIR];
        SKILL_NAME electricity_SkillName = Context.g_MapSelectedSkillByElement_[ELEMENT.ELECTRICITY];

        if (fire_SkillName == SKILL_NAME.FIRE_FIRERAIN)
        {
            skillIcon_Fire_.sprite = skillIcon_Fire_1_;
        }
        else if (fire_SkillName == SKILL_NAME.FIRE_METEOR)
        {
            skillIcon_Fire_.sprite = skillIcon_Fire_2_;
        }

        if (ice_SkillName == SKILL_NAME.ICE_BLIZZARD)
        {
            skillIcon_Ice_.sprite = skillIcon_Ice_1_;
        }
        else if (ice_SkillName == SKILL_NAME.ICE_GLACIALUPRISE)
        {
            skillIcon_Ice_.sprite = skillIcon_Ice_2_;
        }

        if (air_SkillName == SKILL_NAME.AIR_AEROVORTEX)
        {
            skillIcon_Air_.sprite = skillIcon_Air_1_;
        }
        else if (air_SkillName == SKILL_NAME.AIR_TEMPESTBLAST)
        {
            skillIcon_Air_.sprite = skillIcon_Air_2_;
        }

        if (electricity_SkillName == SKILL_NAME.ELEC_CHAINLIGHTNING)
        {
            skillIcon_Electricity_.sprite = skillIcon_Electricity_1_;
        }
        else if (electricity_SkillName == SKILL_NAME.ELEC_THUNDERRESIDUE)
        {
            skillIcon_Electricity_.sprite = skillIcon_Electricity_2_;
        }

    }

    public void OnSelectElementBtnClick_Fire()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        OnStartGame(ELEMENT.FIRE);
    }

    public void OnSelectElementBtnClick_Ice()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        OnStartGame(ELEMENT.ICE);
    }

    public void OnSelectElementBtnClick_Air()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        OnStartGame(ELEMENT.AIR);
    }

    public void OnSelectElementBtnClick_Electricity()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        OnStartGame(ELEMENT.ELECTRICITY);
    }

    void OnStartGame(ELEMENT element)
    {
        Context.selectElement_ = element;

        FadeManager.Instance.SceneChange(Context.GAME_SCENE);
    }
}
