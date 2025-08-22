using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UNIT_NAME
{
    NONE,
    CREATOR_PLAYER,
    MAGICIAN_PLAYER,
    BASE_RED,
    BASE_BLUE,
    SOLDIER,
    ARCHER,
    SHIELDER,
    MONSTER_SOLIDER,
    MONSTER_ARCHER,
    MONSTER_SHIELDER,
    MONSTER_MAGICIAN,
    MONSTER_KNIGHT,
}

public enum UNIT_STATUS
{
    STAY,
    MOVE,
    ATTACK,
    DEAD,
}

public enum TEAM
{
    TEAM_1, // ≥ª ∆¿
    TEAM_2
}

public enum ELEMENT
{
    NONE,
    FIRE,
    AIR,
    ICE,
    ELECTRICITY
}

public class UnitPrefabName
{
    public static string Base_team1 = "Base_team1";
    public static string Base_team2 = "Base_team2";

    public static string Soldier = "Soldier";
    public static string Archer = "Archer";
    public static string Shielder = "Shielder";

    public static string Monster_Soldier = "Monster_Soldier";
    public static string Monster_Archer = "Monster_Archer";
    public static string Monster_Shielder = "Monster_Shielder";
    public static string Monster_Magician = "Monster_Magician";
    public static string Monster_Knight = "Monster_Knight";


    public static string MagicianPlayer = "MagicianPlayer";
}

public class UnitDefine
{
    public static int MAX_COST = 100;
    public static float ENTER_UNIT_TIME = 8.0f;
    public static float MAGICIAN_LANDING_TIME = 25f;

    public static Dictionary<UNIT_NAME, int> mapUnitPoolingCnt_ = new Dictionary<UNIT_NAME, int>()
    {
        { UNIT_NAME.BASE_RED, 2 },
        { UNIT_NAME.BASE_BLUE, 2 },

        { UNIT_NAME.SOLDIER , 50},
        { UNIT_NAME.ARCHER , 50},
        { UNIT_NAME.SHIELDER , 50},

        { UNIT_NAME.MONSTER_SOLIDER , 50},
        { UNIT_NAME.MONSTER_ARCHER , 50},
        { UNIT_NAME.MONSTER_SHIELDER , 50},
        { UNIT_NAME.MONSTER_MAGICIAN , 50},
        { UNIT_NAME.MONSTER_KNIGHT , 2},

        { UNIT_NAME.MAGICIAN_PLAYER , 1},
    };

    public static Dictionary<UNIT_NAME, int> mapUnitCost_ = new Dictionary<UNIT_NAME, int>()
    {
        { UNIT_NAME.SOLDIER , 5},
        { UNIT_NAME.ARCHER , 10},
        { UNIT_NAME.SHIELDER , 7},
        { UNIT_NAME.MAGICIAN_PLAYER , 0},
    };

    public static Dictionary<UNIT_NAME, float> mapUnitHitArange_ = new Dictionary<UNIT_NAME, float>()
    {
        { UNIT_NAME.BASE_RED, 2.5f },
        { UNIT_NAME.BASE_BLUE, 2.5f },

        { UNIT_NAME.SOLDIER, 0.2f },
        { UNIT_NAME.ARCHER , 0.2f},
        { UNIT_NAME.SHIELDER , 0.2f},

        { UNIT_NAME.MONSTER_SOLIDER , 0.2f},
        { UNIT_NAME.MONSTER_ARCHER , 0.2f},
        { UNIT_NAME.MONSTER_SHIELDER , 0.2f},
        { UNIT_NAME.MONSTER_MAGICIAN , 0.2f},
        { UNIT_NAME.MONSTER_KNIGHT , 0.4f},

        { UNIT_NAME.MAGICIAN_PLAYER, 0.2f },
    };

    // UnitPosition ±‚¡ÿ
    public static Dictionary<UNIT_NAME, float> mapUnitCreateYPosition_ = new Dictionary<UNIT_NAME, float>()
    {
        { UNIT_NAME.CREATOR_PLAYER, 15f },
        { UNIT_NAME.MAGICIAN_PLAYER, 0f },

        { UNIT_NAME.BASE_RED, 0f },
        { UNIT_NAME.BASE_BLUE, 0f },

        { UNIT_NAME.SOLDIER, 0f },
        { UNIT_NAME.ARCHER , 0f},
        { UNIT_NAME.SHIELDER , 0f},

        { UNIT_NAME.MONSTER_SOLIDER , 0f},
        { UNIT_NAME.MONSTER_ARCHER , 0f},
        { UNIT_NAME.MONSTER_SHIELDER , 0f},
        { UNIT_NAME.MONSTER_MAGICIAN , 0f},
        { UNIT_NAME.MONSTER_KNIGHT , 0f},
    };

    public static Dictionary<UNIT_NAME, float> mapUnitHitYPosition_ = new Dictionary<UNIT_NAME, float>()
    {
        { UNIT_NAME.CREATOR_PLAYER, 0f },
        { UNIT_NAME.MAGICIAN_PLAYER, 1f },

        { UNIT_NAME.BASE_RED, 3f },
        { UNIT_NAME.BASE_BLUE, 3f },

        { UNIT_NAME.SOLDIER, 0.7f },
        { UNIT_NAME.ARCHER , 0.7f },
        { UNIT_NAME.SHIELDER , 0.7f },

        { UNIT_NAME.MONSTER_SOLIDER , 0.7f },
        { UNIT_NAME.MONSTER_ARCHER , 0.7f },
        { UNIT_NAME.MONSTER_SHIELDER , 0.7f },
        { UNIT_NAME.MONSTER_MAGICIAN , 0.7f },
        { UNIT_NAME.MONSTER_KNIGHT , 0.7f },
    };
}
