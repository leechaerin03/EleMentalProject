using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SKILL_NAME
{
    NONE,
    
    AIR_AEROVORTEX,
    AIR_TEMPESTBLAST,
    ELEC_CHAINLIGHTNING,
    ELEC_THUNDERRESIDUE,
    FIRE_FIRERAIN,
    FIRE_METEOR,
    ICE_BLIZZARD,
    ICE_GLACIALUPRISE
}

public enum SKILL_USE_TYPE
{
    IMMEDIATE,
    SELECT_TARGET,
}

public class SkillPrefabName
{
    
    public static string Air_AeroVortex = "Air_AeroVortex";
    public static string Air_TempestBlast = "Air_TempestBlast";
    public static string Elec_ChainLightning = "Elec_ChainLightning";
    public static string Elec_ThunderResidue = "Elec_ThunderResidue";
    public static string Fire_FireRain = "Fire_FireRain";
    public static string Fire_Meteor = "Fire_Meteor";
    public static string Ice_Blizzard = "Ice_Blizzard";
    public static string Ice_GlacialUprise = "Ice_GlacialUprise";
}

public class SkillDefine
{
    public static Dictionary<SKILL_NAME, int> mapSkillPoolingCnt_ = new Dictionary<SKILL_NAME, int>()
    {
        { SKILL_NAME.AIR_AEROVORTEX, 3 },
        { SKILL_NAME.AIR_TEMPESTBLAST, 3 },
        { SKILL_NAME.ELEC_CHAINLIGHTNING, 3 },
        { SKILL_NAME.ELEC_THUNDERRESIDUE, 3 },
        { SKILL_NAME.FIRE_FIRERAIN, 3 },
        { SKILL_NAME.FIRE_METEOR, 3 },
        { SKILL_NAME.ICE_BLIZZARD, 3 },
        { SKILL_NAME.ICE_GLACIALUPRISE, 3 },
    };

    public static Dictionary<SKILL_NAME, float> mapSkillCooltime_ = new Dictionary<SKILL_NAME, float>()
    {
        { SKILL_NAME.AIR_AEROVORTEX,        7f },
        { SKILL_NAME.AIR_TEMPESTBLAST,      12f },
        { SKILL_NAME.ELEC_CHAINLIGHTNING,   4f },
        { SKILL_NAME.ELEC_THUNDERRESIDUE,   11f },
        { SKILL_NAME.FIRE_FIRERAIN,         2f },
        { SKILL_NAME.FIRE_METEOR,           8f },
        { SKILL_NAME.ICE_BLIZZARD,          12f },
        { SKILL_NAME.ICE_GLACIALUPRISE,     15f },
    };

    // SkillPosition ±‚¡ÿ
    public static Dictionary<SKILL_NAME, float> mapSkillCreateYPosition_ = new Dictionary<SKILL_NAME, float>()
    {
        { SKILL_NAME.AIR_AEROVORTEX,        0.1f },
        { SKILL_NAME.AIR_TEMPESTBLAST,      0.1f },
        { SKILL_NAME.ELEC_CHAINLIGHTNING,   0.5f },
        { SKILL_NAME.ELEC_THUNDERRESIDUE,   0.1f },
        { SKILL_NAME.FIRE_FIRERAIN,         0.5f },
        { SKILL_NAME.FIRE_METEOR,           0.1f },
        { SKILL_NAME.ICE_BLIZZARD,          0.1f },
        { SKILL_NAME.ICE_GLACIALUPRISE,     0.1f },
    };

    public static Dictionary<SKILL_NAME, SKILL_USE_TYPE> mapSkillUsingType_ = new Dictionary<SKILL_NAME, SKILL_USE_TYPE>()
    { 
        { SKILL_NAME.AIR_AEROVORTEX,        SKILL_USE_TYPE.SELECT_TARGET },
        { SKILL_NAME.AIR_TEMPESTBLAST,      SKILL_USE_TYPE.IMMEDIATE },
        { SKILL_NAME.ELEC_CHAINLIGHTNING,   SKILL_USE_TYPE.IMMEDIATE },
        { SKILL_NAME.ELEC_THUNDERRESIDUE,   SKILL_USE_TYPE.SELECT_TARGET },
        { SKILL_NAME.FIRE_FIRERAIN,         SKILL_USE_TYPE.IMMEDIATE },
        { SKILL_NAME.FIRE_METEOR,           SKILL_USE_TYPE.SELECT_TARGET },
        { SKILL_NAME.ICE_BLIZZARD,          SKILL_USE_TYPE.SELECT_TARGET },
        { SKILL_NAME.ICE_GLACIALUPRISE,     SKILL_USE_TYPE.SELECT_TARGET },
    };
}