using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] Transform enemySpawnCenterTransform_;
    [SerializeField] float spawnRandomRangeX_;
    [SerializeField] float spawnRandomRangeZ_;

    bool isGameEnded_;
    public int stage_;
    List<MonsterSpawnInfo> stageMonsterSpawnInfoList_;

    Dictionary<MonsterSpawnInfo, float> mapElapsedTime_ = new Dictionary<MonsterSpawnInfo, float>();
    Dictionary<MonsterSpawnInfo, int> mapElapsedSpawnCnt_ = new Dictionary<MonsterSpawnInfo, int>();

    public void Init(int stage)
    {
        isGameEnded_ = false;
        stage_ = stage;
        stageMonsterSpawnInfoList_ = StageInfo.mapStageMonsterSpawnInfo_[stage];
        mapElapsedTime_.Clear();

        for(int i = 0; i < stageMonsterSpawnInfoList_.Count; ++i)
        {
            mapElapsedTime_.Add(stageMonsterSpawnInfoList_[i], 0);
            mapElapsedSpawnCnt_.Add(stageMonsterSpawnInfoList_[i], stageMonsterSpawnInfoList_[i].RepeatCnt == 0 ? 100000 : stageMonsterSpawnInfoList_[i].RepeatCnt);
        }
    }

    private void Update()
    {
        if (isGameEnded_)
            return;

        for(int i = 0; i < stageMonsterSpawnInfoList_.Count; ++i)
        {
            MonsterSpawnInfo info = stageMonsterSpawnInfoList_[i];

            mapElapsedTime_[info] += Time.smoothDeltaTime;
            if(mapElapsedTime_[info] >= info.DelayTime && 
                mapElapsedSpawnCnt_[info] > 0)
            {
                mapElapsedTime_[info] -= info.DelayTime;
                mapElapsedSpawnCnt_[info] -= 1;

                UNIT_NAME unitName = info.UnitName;
                UnitData unitData;
                UnitAttackData unitAttackData;

                //  Random.Range(2.0f. 2.5f);
                // 위의 코드 사용하면 랜덤값 넣을 수 잇음! 필요하면 참고할 것


                if (unitName == UNIT_NAME.MONSTER_SOLIDER)
                {
                    unitData = new UnitData(unitName, 2.5f, 50 + stage_ * 10, TEAM.TEAM_2);
                    unitAttackData = new UnitAttackData(10 + stage_ * 10, 2f, 2.0f);
                }
                else if (unitName == UNIT_NAME.MONSTER_ARCHER)
                {
                    unitData = new UnitData(unitName, 2.0f, 80 + stage_ * 10, TEAM.TEAM_2);
                    unitAttackData = new UnitAttackData(20 + stage_ * 10, 8f, 2.5f);
                }
                else if (unitName == UNIT_NAME.MONSTER_SHIELDER)
                {
                    unitData = new UnitData(unitName, 2.5f, 100 + stage_ * 10, TEAM.TEAM_2);
                    unitAttackData = new UnitAttackData(5 + stage_ * 10, 1f, 3.0f);
                }
                else if (unitName == UNIT_NAME.MONSTER_MAGICIAN)
                {
                    unitData = new UnitData(unitName, 2.5f, 200 + stage_ * 10, TEAM.TEAM_2);
                    unitAttackData = new UnitAttackData(30 + stage_ * 10, 10f, 3.0f);
                }
                else if (unitName == UNIT_NAME.MONSTER_KNIGHT)
                {
                    unitData = new UnitData(unitName, 2.5f, 500 /*+ stage_ * 10*/, TEAM.TEAM_2);
                    unitAttackData = new UnitAttackData(100 /*+ stage_ * 2*/, 4f, 3.0f);
                }
                else
                {
                    Debug.LogError("UnitName error");
                    unitData = new UnitData(unitName, 0, 0 + stage_ * 2, TEAM.TEAM_2);
                    unitAttackData = new UnitAttackData(8 + stage_ * 2, 3f, 2.2f);
                }

                if(Context.g_IsPracticeMode_)
                {
                    unitData = new UnitData(unitName, 0, 2000 + stage_ * 10, TEAM.TEAM_2);
                    unitAttackData = new UnitAttackData(0 + stage_ * 10, 0, 10f);
                }


                for(int j = 0; j < info.SpawnCntAtOnce; ++j)
                {
                    Vector3 makePosition = new Vector3(Random.Range(-spawnRandomRangeX_, spawnRandomRangeX_),
                        UnitDefine.mapUnitCreateYPosition_[unitName],
                        Random.Range(-spawnRandomRangeZ_, spawnRandomRangeZ_));

                    Hub_Ingame.Instance.unitController_.UseUnit(unitData, unitAttackData, enemySpawnCenterTransform_.position + makePosition);
                }
            }
        }
    }

    public void OnGameEnded()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.LoseSound);
        isGameEnded_ = true;
    }
}
