using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFFECT_NAME
{
    NONE,

    DAMAGE_NUMBER,

    UNIT_DEAD_EFFECT,

    HIT_NONE_ELEMENT,
    HIT_FIRE_ELEMENT,
    HIT_ICE_ELEMENT,
    HIT_AIR_ELEMENT,
    HIT_ELECTRICITY_ELEMENT,

    ICE_CRISTAL,

    PROJECTILE_ARROW,
    PROJECTILE_MAGIC_NONE,
    PROJECTILE_MAGIC_FIRERAIN,
    PROJECTILE_MAGIC_THUNDER,

    CIRCLE_REFRECTION,
    CIRCLE_REFRECTION_FROM_CAMERA,
}

public class EffectPrefabName
{
    public static string Damage = "Damage";

    public static string UnitDeadEffect = "UnitDeadEffect";

    public static string HitEffect_NONE = "Hit_None";
    public static string HitEffect_FIRE = "Hit_Fire";
    public static string HitEffect_ICE = "Hit_Ice";
    public static string HitEffect_AIR = "Hit_Air";
    public static string HitEffect_ELECTRICITY = "Hit_Electricity";

    public static string Ice_Cristal = "Ice_Cristal";

    public static string Projectile_Arrow = "Projectile_Arrow";
    public static string Projectile_Magic_None = "Projectile_Magic_None";
    public static string Projectile_Magic_FireRain = "Projectile_Magic_FireRain";
    public static string Projectile_Magic_Thunder = "Projectile_Magic_Thunder";

    public static string CircleRefrection = "CircleRefrection"; 
    public static string CircleRefrectionFromCamera = "CircleRefrectionFromCamera";
}

public class EffectDefine
{
    public static Dictionary<EFFECT_NAME, int> mapEffectPoolingCnt_ = new Dictionary<EFFECT_NAME, int>()
    {
        { EFFECT_NAME.DAMAGE_NUMBER, 100 },

        { EFFECT_NAME.UNIT_DEAD_EFFECT, 20 },

        { EFFECT_NAME.HIT_NONE_ELEMENT, 50 },
        { EFFECT_NAME.HIT_FIRE_ELEMENT, 50 },
        { EFFECT_NAME.HIT_ICE_ELEMENT, 50 },
        { EFFECT_NAME.HIT_AIR_ELEMENT, 50 },
        { EFFECT_NAME.HIT_ELECTRICITY_ELEMENT, 50 },

        { EFFECT_NAME.ICE_CRISTAL, 2 },

        { EFFECT_NAME.PROJECTILE_ARROW, 50 },
        { EFFECT_NAME.PROJECTILE_MAGIC_NONE, 20 },
        { EFFECT_NAME.PROJECTILE_MAGIC_FIRERAIN, 30 },
        { EFFECT_NAME.PROJECTILE_MAGIC_THUNDER, 30 },

        { EFFECT_NAME.CIRCLE_REFRECTION, 5 },
        { EFFECT_NAME.CIRCLE_REFRECTION_FROM_CAMERA, 5 },
    };
}
