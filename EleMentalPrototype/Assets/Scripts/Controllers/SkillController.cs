using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SkillController : MonoBehaviour
{
    [SerializeField] Transform skillPoolingParentTransform_;
    [SerializeField] Transform skillAliveParentTransform_;


    Dictionary<SKILL_NAME, string> mapSkillPrefabName_ = new Dictionary<SKILL_NAME, string>();

    readonly Dictionary<SKILL_NAME, int> mapSkillPoolingCnt_ = SkillDefine.mapSkillPoolingCnt_;

    Dictionary<SKILL_NAME, List<Skill>> mapSkillList_;
    List<Skill> listUsingSkills_ = new List<Skill>();

    private void Awake()
    {
        FactoryInit();
        UnitNameInit();

    }

    void FactoryInit()
    {
        mapSkillList_ = new Dictionary<SKILL_NAME, List<Skill>>();
        
        mapSkillList_.Add(SKILL_NAME.AIR_AEROVORTEX, new List<Skill>());
        mapSkillList_.Add(SKILL_NAME.AIR_TEMPESTBLAST, new List<Skill>());
        mapSkillList_.Add(SKILL_NAME.ELEC_CHAINLIGHTNING, new List<Skill>());
        mapSkillList_.Add(SKILL_NAME.ELEC_THUNDERRESIDUE, new List<Skill>());
        mapSkillList_.Add(SKILL_NAME.FIRE_FIRERAIN, new List<Skill>());
        mapSkillList_.Add(SKILL_NAME.FIRE_METEOR, new List<Skill>());
        mapSkillList_.Add(SKILL_NAME.ICE_BLIZZARD, new List<Skill>());
        mapSkillList_.Add(SKILL_NAME.ICE_GLACIALUPRISE, new List<Skill>());
    }

    void UnitNameInit()
    {
        mapSkillPrefabName_.Add(SKILL_NAME.AIR_AEROVORTEX, SkillPrefabName.Air_AeroVortex);
        mapSkillPrefabName_.Add(SKILL_NAME.AIR_TEMPESTBLAST, SkillPrefabName.Air_TempestBlast);
        mapSkillPrefabName_.Add(SKILL_NAME.ELEC_CHAINLIGHTNING, SkillPrefabName.Elec_ChainLightning);
        mapSkillPrefabName_.Add(SKILL_NAME.ELEC_THUNDERRESIDUE, SkillPrefabName.Elec_ThunderResidue);
        mapSkillPrefabName_.Add(SKILL_NAME.FIRE_FIRERAIN, SkillPrefabName.Fire_FireRain);
        mapSkillPrefabName_.Add(SKILL_NAME.FIRE_METEOR, SkillPrefabName.Fire_Meteor);
        mapSkillPrefabName_.Add(SKILL_NAME.ICE_BLIZZARD, SkillPrefabName.Ice_Blizzard);
        mapSkillPrefabName_.Add(SKILL_NAME.ICE_GLACIALUPRISE, SkillPrefabName.Ice_GlacialUprise);

    }

    void AddSkillPoolingList(SKILL_NAME skillName)
    {
        if (skillName == SKILL_NAME.NONE)
        {
            Assert.IsTrue(true);
            return;
        }

        List<Skill> listSkill = mapSkillList_[skillName];
        int cnt = mapSkillPoolingCnt_[skillName];

        for (int i = 0; i < cnt; ++i)
        {
            Skill skill = CreateSkill(skillName);
            listSkill.Add(skill);
        }
    }

    Skill CreateSkill(SKILL_NAME skillName)
    {
        Skill skill;

        string prefabName;
        if (mapSkillPrefabName_.TryGetValue(skillName, out prefabName) == false)
        {
            skill = new Skill();
            Assert.IsTrue(true);
        }
        else
        {
            GameObject skillPrefab = Resources.Load(prefabName) as GameObject;

            skill = Instantiate(skillPrefab).GetComponent<Skill>();
        }

        skill.transform.parent = skillPoolingParentTransform_;
        skill.SkillPosition = Vector3.zero;
        skill.transform.localEulerAngles = Vector3.zero;
        skill.transform.localScale = Vector3.one;
        skill.gameObject.SetActive(false);

        return skill;
    }

    Skill UseSkill(SKILL_NAME skillName)
    {
        List<Skill> listSkills = mapSkillList_[skillName];
        if (listSkills.Count <= 0)
        {
            AddSkillPoolingList(skillName);
        }

        Skill skill = listSkills[0];
        listSkills.RemoveAt(0);
        listUsingSkills_.Add(skill);

        skill.transform.parent = skillAliveParentTransform_;
        skill.SkillPosition = Vector3.zero;
        skill.transform.localEulerAngles = Vector3.zero;
        skill.transform.localScale = Vector3.one;
        skill.gameObject.SetActive(true);

        return skill;
    }

    public Skill UseSkill(SKILL_NAME skillName, Vector3 skillPosition, int damage, List<TEAM> targetTeamList)
    {
        Skill skill = UseSkill(skillName);
        skill.Init(skillName, skillPosition, damage, targetTeamList);

        return skill;
    }

    public Skill UseSkill(SKILL_NAME skillName, Vector3 skillPosition, int damage, List<TEAM> targetTeamList, int hitCnt)
    {
        Skill skill = UseSkill(skillName);
        skill.Init(skillName, skillPosition, damage, targetTeamList, hitCnt);

        return skill;
    }

    public void RestoreSkill(Skill skill)
    {
        SKILL_NAME skillName = skill.SkillName;
        skill.gameObject.SetActive(false);
        skill.transform.parent = skillPoolingParentTransform_;

        if (listUsingSkills_.Remove(skill))
        {
            mapSkillList_[skillName].Add(skill);
        }
    }
}
