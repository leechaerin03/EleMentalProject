using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Context
{
    // Container
    public static string LOBBY_SCENE = "LobbyScene";
    public static string GAME_SCENE = "GameScene";

    public static int g_selectChapter_;
    public static int g_selectStage_;

    public static ELEMENT selectElement_ = ELEMENT.NONE;

    public static bool g_IsPracticeMode_;

    // SaveData
    static int g_Level_;
    public static int Level
    {
        get { return g_Level_; }
        set
        {
            g_Level_ = value;
            PlayerPrefs.SetInt("Level", g_Level_);
        }
    }

    static int g_Coin_;
    public static int Coin
    {
        get { return g_Coin_; }
        set
        {
            g_Coin_ = value;
            PlayerPrefs.SetInt("Coin", g_Coin_);
        }
    }

    static int g_Fire_SkillLevel_;
    static int g_Ice_SkillLevel_;
    static int g_Air_SkillLevel_;
    static int g_Electricity_SkillLevel_;
    public static Dictionary<ELEMENT, int> g_mapSkillLevel_ = new Dictionary<ELEMENT, int>();

    
    public static Dictionary<ELEMENT, SKILL_NAME> g_MapSelectedSkillByElement_ = new Dictionary<ELEMENT, SKILL_NAME>();


    public static void OnLoadContextData()
    {
        Coin = PlayerPrefs.GetInt("Coin", 100);
        Level = PlayerPrefs.GetInt("Level", 1);

        g_Fire_SkillLevel_ = PlayerPrefs.GetInt(ELEMENT.FIRE + "_SkillLevel", 1);
        g_Ice_SkillLevel_ = PlayerPrefs.GetInt(ELEMENT.ICE + "_SkillLevel", 1);
        g_Air_SkillLevel_ = PlayerPrefs.GetInt(ELEMENT.AIR + "_SkillLevel", 1);
        g_Electricity_SkillLevel_ = PlayerPrefs.GetInt(ELEMENT.ELECTRICITY + "_SkillLevel", 1);

        g_mapSkillLevel_.Clear();
        g_mapSkillLevel_.Add(ELEMENT.FIRE, g_Fire_SkillLevel_);
        g_mapSkillLevel_.Add(ELEMENT.ICE, g_Ice_SkillLevel_);
        g_mapSkillLevel_.Add(ELEMENT.AIR, g_Air_SkillLevel_);
        g_mapSkillLevel_.Add(ELEMENT.ELECTRICITY, g_Electricity_SkillLevel_);


        g_MapSelectedSkillByElement_.Clear();
        g_MapSelectedSkillByElement_.Add(ELEMENT.FIRE, (SKILL_NAME)Enum.Parse(typeof(SKILL_NAME), PlayerPrefs.GetString(ELEMENT.FIRE + "_Saved_Skill_Name", SKILL_NAME.FIRE_FIRERAIN.ToString())));
        g_MapSelectedSkillByElement_.Add(ELEMENT.ICE, (SKILL_NAME)Enum.Parse(typeof(SKILL_NAME), PlayerPrefs.GetString(ELEMENT.ICE + "_Saved_Skill_Name", SKILL_NAME.ICE_BLIZZARD.ToString())));
        g_MapSelectedSkillByElement_.Add(ELEMENT.AIR, (SKILL_NAME)Enum.Parse(typeof(SKILL_NAME), PlayerPrefs.GetString(ELEMENT.AIR + "_Saved_Skill_Name", SKILL_NAME.AIR_AEROVORTEX.ToString())));
        g_MapSelectedSkillByElement_.Add(ELEMENT.ELECTRICITY, (SKILL_NAME)Enum.Parse(typeof(SKILL_NAME), PlayerPrefs.GetString(ELEMENT.ELECTRICITY + "_Saved_Skill_Name", SKILL_NAME.ELEC_CHAINLIGHTNING.ToString())));



        CheatModeOn();

    }

    static void CheatModeOn()
    {
        Coin = 20192022;
        Level = 4;
        PlayerPrefs.SetInt(StageController.COMPLETED_STAGE_KEY, 15);
    }

    public static void SkillLevelUp(ELEMENT element)
    {
        if (g_mapSkillLevel_.ContainsKey(element) == false)
            return;

        g_mapSkillLevel_[element] += 1;
        PlayerPrefs.SetInt(element + "_SkillLevel", g_mapSkillLevel_[element]);
    }

    public static void OnClickSelectUsingSkill(ELEMENT element, SKILL_NAME skillName)
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        g_MapSelectedSkillByElement_[element] = skillName;

        Debug.Log(skillName.ToString());
        PlayerPrefs.SetString(element + "_Saved_Skill_Name", skillName.ToString());
    }

    //[MenuItem("LocalData/Reset Local Save Data")]
    //public static void ResetPlayerPrefab()
    //{
    //    PlayerPrefs.DeleteAll();
    //}
}
