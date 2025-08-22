using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnInfo
{
    UNIT_NAME unitName_;
    float delayTime_;
    int spawnCntAtOnce_;
    int repeatCnt_; // 0 is not limit

    public UNIT_NAME UnitName
    {
        get { return unitName_; }
    }

    public float DelayTime
    {
        get { return delayTime_; }
    }

    public int SpawnCntAtOnce
    {
        get { return spawnCntAtOnce_; }
    }

    public int RepeatCnt
    {
        get { return repeatCnt_; }
    }

    public MonsterSpawnInfo(UNIT_NAME unitName, float delayTime, int spawnCntAtOnce, int repeatCnt = 0)
    {
        unitName_ = unitName;
        delayTime_ = delayTime;
        spawnCntAtOnce_ = spawnCntAtOnce;
        repeatCnt_ = repeatCnt;
    }
}

public class StageInfo
{
    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_1_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 3),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_2_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 3),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_MAGICIAN, 15f, 1),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_KNIGHT, 20f, 1,1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_3_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_4_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_5_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_MAGICIAN, 15f, 1),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_KNIGHT, 20f, 1,1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_6_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_7_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_8_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_9_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_10_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_MAGICIAN, 15f, 1),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_KNIGHT, 20f, 1,1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_11_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_12_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_13_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_14_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1)
    };

    static List<MonsterSpawnInfo> monsterSpawnInfo_Stage_15_ = new List<MonsterSpawnInfo>()
    {
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SOLIDER, 10f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_ARCHER, 15f, 2),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_SHIELDER, 8f, 1),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_MAGICIAN, 15f, 1),
        new MonsterSpawnInfo(UNIT_NAME.MONSTER_KNIGHT, 20f, 1,1)
    };


    public static Dictionary<int, List<MonsterSpawnInfo>> mapStageMonsterSpawnInfo_ = new Dictionary<int, List<MonsterSpawnInfo>>()
    {
        { 1, monsterSpawnInfo_Stage_1_},
        { 2, monsterSpawnInfo_Stage_2_},
        { 3, monsterSpawnInfo_Stage_3_},
        { 4, monsterSpawnInfo_Stage_4_},
        { 5, monsterSpawnInfo_Stage_5_},
        { 6, monsterSpawnInfo_Stage_6_},
        { 7, monsterSpawnInfo_Stage_7_},
        { 8, monsterSpawnInfo_Stage_8_},
        { 9, monsterSpawnInfo_Stage_9_},
        { 10, monsterSpawnInfo_Stage_10_},
        { 11, monsterSpawnInfo_Stage_11_},
        { 12, monsterSpawnInfo_Stage_12_},
        { 13, monsterSpawnInfo_Stage_13_},
        { 14, monsterSpawnInfo_Stage_14_},
        { 15, monsterSpawnInfo_Stage_15_},
    };
}
