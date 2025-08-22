using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : Popup
{
    [SerializeField] Transform inventoryPopupGrid_;
    [SerializeField] Text levelText_;

    List<SkillInventoryBox> skillInventoryBoxList_ = new List<SkillInventoryBox>();

    List<ELEMENT> elementList_ = new List<ELEMENT>()
        {
            ELEMENT.FIRE , ELEMENT.FIRE ,
            ELEMENT.ICE , ELEMENT.ICE ,
            ELEMENT.AIR , ELEMENT.AIR ,
            ELEMENT.ELECTRICITY , ELEMENT.ELECTRICITY ,
        };

    List<SKILL_NAME> skillNameList_ = new List<SKILL_NAME>()
        {
            SKILL_NAME.FIRE_FIRERAIN, SKILL_NAME.FIRE_METEOR,
            SKILL_NAME.ICE_BLIZZARD, SKILL_NAME.ICE_GLACIALUPRISE,
            SKILL_NAME.AIR_AEROVORTEX, SKILL_NAME.AIR_TEMPESTBLAST,
            SKILL_NAME.ELEC_CHAINLIGHTNING, SKILL_NAME.ELEC_THUNDERRESIDUE,
        };

    public override void Init()
    {
        base.Init();

        levelText_.text = Context.Level.ToString();
    }

    public void CreateSkillIventoryBox()
    {
        GameObject creatInventoryBox = Resources.Load("SkillInventoryBox") as GameObject;

        if (creatInventoryBox == null)
        {
            Debug.LogError("creatInventoryBox is not exist");
            return;
        }

        Action onSlotClickCallback = delegate ()
        {
            RefreshSkillInventorySlots();
        };


        for(int i = 0; i < 8; ++i)
        {
            SkillInventoryBox skillInventoryBox = Instantiate(creatInventoryBox).GetComponent<SkillInventoryBox>();
            skillInventoryBox.transform.parent = inventoryPopupGrid_;

            skillInventoryBox.Init(elementList_[i], skillNameList_[i], onSlotClickCallback);
            skillInventoryBoxList_.Add(skillInventoryBox);
        }
    }

    void RefreshSkillInventorySlots()
    {
        Action onSlotClickCallback = delegate ()
        {
            RefreshSkillInventorySlots();
        };

        for (int i = 0; i < 8; ++i)
        {
            skillInventoryBoxList_[i].Init(elementList_[i], skillNameList_[i], onSlotClickCallback);
        }
    }


}
