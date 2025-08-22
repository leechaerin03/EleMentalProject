using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] Transform team_1_Base_Transform_;
    public Vector3 Team_1_Base_UnitPosition
    {
        get { return team_1_Base_Transform_.localPosition; }
    }

    [SerializeField] Transform team_2_Base_Transform_;
    public Vector3 Team_2_Base_UnitPosition
    {
        get { return team_2_Base_Transform_.localPosition; }
    }

    [SerializeField] GameObject team_1_Createable_Ground_;
    public bool Team_1_Createable_Ground_Active
    {
        set { team_1_Createable_Ground_.SetActive(value); }
    }

    [SerializeField] Stage curStage_;
    public Stage CurStage
    {
        get { return curStage_; }
    }

    private void Start()
    {
        int hp = 1000 + (curStage_.stage_ * 100);
        if (Context.g_IsPracticeMode_)
        {
            hp = 100000000;
        }

        // Base Create
        UnitData base_1_unitData = new UnitData(UNIT_NAME.BASE_BLUE, 0, hp, TEAM.TEAM_1);
        Unit team_1_base = Hub_Ingame.Instance.unitController_.UseUnit(base_1_unitData, Team_1_Base_UnitPosition);
        team_1_base.SetDeadCallback(delegate ()
        {
            BaseDestroy(TEAM.TEAM_1);
        });


        UnitData base_2_unitData = new UnitData(UNIT_NAME.BASE_RED, 0, hp, TEAM.TEAM_2);
        Unit team_2_base = Hub_Ingame.Instance.unitController_.UseUnit(base_2_unitData, Team_2_Base_UnitPosition);
        team_2_base.SetDeadCallback(delegate ()
        {
            BaseDestroy(TEAM.TEAM_2);
        });
    }

    void BaseDestroy(TEAM team)
    {
        Hub_Ingame.Instance.stageController_.BaseDestroy(team);
    }
}
