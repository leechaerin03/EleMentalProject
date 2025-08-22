using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UnitController : MonoBehaviour
{

    [SerializeField] Transform unitPoolingParentTransform_;
    [SerializeField] Transform unitAliveParentTransform_;

    [Header("Team Info")]
    [SerializeField] Color team_1_Color_;
    [SerializeField] Color team_2_Color_;

    Dictionary<TEAM, Color> mapTeamColor_ = new Dictionary<TEAM, Color>();
    //[SerializeField] Material team_1_ColorMaterial_;
    //[SerializeField] Material team_2_ColorMaterial_;

    // Dictionary<TEAM, Material> mapTeamColorMaterial_ = new Dictionary<TEAM, Material>();


    [Header("Unit Prefab")]
    [SerializeField] Unit_CreatorPlayer creatorPlayerPrefab_;

    Dictionary<UNIT_NAME, string> mapUnitPrefabName_ = new Dictionary<UNIT_NAME, string>();

    readonly Dictionary<UNIT_NAME, int> mapUnitPoolingCnt_ = UnitDefine.mapUnitPoolingCnt_;

    Dictionary<UNIT_NAME, List<Unit>> mapUnitList_;
    List<Unit> listUsingUnits_ = new List<Unit>();
    
    private void Awake()
    {
        TeamInfoInit();

        FactoryInit();
        UnitNameInit();
    }

    private void Start()
    {
        CreatorPlayerInit();
    }

    void TeamInfoInit()
    {
        // mapTeamColorMaterial_.Add(TEAM.TEAM_1, team_1_ColorMaterial_);
        // mapTeamColorMaterial_.Add(TEAM.TEAM_2, team_2_ColorMaterial_);
        mapTeamColor_.Add(TEAM.TEAM_1, team_1_Color_);
        mapTeamColor_.Add(TEAM.TEAM_2, team_2_Color_);
    }

    void FactoryInit()
    {
        mapUnitList_ = new Dictionary<UNIT_NAME, List<Unit>>();
        
        mapUnitList_.Add(UNIT_NAME.BASE_RED, new List<Unit>());
        mapUnitList_.Add(UNIT_NAME.BASE_BLUE, new List<Unit>());

        mapUnitList_.Add(UNIT_NAME.SOLDIER, new List<Unit>());
        mapUnitList_.Add(UNIT_NAME.ARCHER, new List<Unit>());
        mapUnitList_.Add(UNIT_NAME.SHIELDER, new List<Unit>());

        mapUnitList_.Add(UNIT_NAME.MAGICIAN_PLAYER, new List<Unit>());

        mapUnitList_.Add(UNIT_NAME.MONSTER_SOLIDER, new List<Unit>());
        mapUnitList_.Add(UNIT_NAME.MONSTER_ARCHER, new List<Unit>());
        mapUnitList_.Add(UNIT_NAME.MONSTER_SHIELDER, new List<Unit>());
        mapUnitList_.Add(UNIT_NAME.MONSTER_MAGICIAN, new List<Unit>());
        mapUnitList_.Add(UNIT_NAME.MONSTER_KNIGHT, new List<Unit>());
    }

    void UnitNameInit()
    {

        mapUnitPrefabName_.Add(UNIT_NAME.BASE_RED, UnitPrefabName.Base_team2);
        mapUnitPrefabName_.Add(UNIT_NAME.BASE_BLUE, UnitPrefabName.Base_team1);

        mapUnitPrefabName_.Add(UNIT_NAME.SOLDIER, UnitPrefabName.Soldier);
        mapUnitPrefabName_.Add(UNIT_NAME.ARCHER, UnitPrefabName.Archer);
        mapUnitPrefabName_.Add(UNIT_NAME.SHIELDER, UnitPrefabName.Shielder);

        mapUnitPrefabName_.Add(UNIT_NAME.MAGICIAN_PLAYER, UnitPrefabName.MagicianPlayer);

        mapUnitPrefabName_.Add(UNIT_NAME.MONSTER_SOLIDER, UnitPrefabName.Monster_Soldier);
        mapUnitPrefabName_.Add(UNIT_NAME.MONSTER_ARCHER, UnitPrefabName.Monster_Archer);
        mapUnitPrefabName_.Add(UNIT_NAME.MONSTER_SHIELDER, UnitPrefabName.Monster_Shielder);
        mapUnitPrefabName_.Add(UNIT_NAME.MONSTER_MAGICIAN, UnitPrefabName.Monster_Magician);
        mapUnitPrefabName_.Add(UNIT_NAME.MONSTER_KNIGHT, UnitPrefabName.Monster_Knight);
    }

    void CreatorPlayerInit()
    {
        Unit_CreatorPlayer playerUnit = Instantiate(creatorPlayerPrefab_, unitAliveParentTransform_);
        playerUnit.UnitPosition = new Vector3(0, -14f, -26f);
        playerUnit.CameraHolder.GetUnitModuleTransform().localEulerAngles = new Vector3(0, 40, 0);

        UnitData unitData = new UnitData(UNIT_NAME.CREATOR_PLAYER, 5f, int.MaxValue, TEAM.TEAM_1, ELEMENT.NONE, false);
        playerUnit.gameObject.SetActive(true);
        playerUnit.Init(unitData);

        Hub_Ingame.Instance.cameraController_.SetCameraHolding(playerUnit.CameraHolder);
    }

    void AddUnitPoolingList(UNIT_NAME unitName)
    {
        if (unitName == UNIT_NAME.NONE)
        {
            Assert.IsTrue(true);
            return;
        }

        List<Unit> listUnit = mapUnitList_[unitName];
        int cnt = mapUnitPoolingCnt_[unitName];

        for (int i = 0; i < cnt; ++i)
        {
            Unit unit = CreateUnit(unitName);
            listUnit.Add(unit);
        }
    }
    
    Unit CreateUnit(UNIT_NAME unitName)
    {
        Unit unit;

        string prefabName;
        if(mapUnitPrefabName_.TryGetValue(unitName, out prefabName) == false)
        {
            unit = new Unit();
            Assert.IsTrue(true);
        }
        else
        {
            GameObject unitPrefab = Resources.Load(prefabName) as GameObject;

            unit = Instantiate(unitPrefab).GetComponent<Unit>();
        }

        unit.transform.parent = unitPoolingParentTransform_;
        unit.UnitPosition = Vector3.zero;
        unit.transform.localEulerAngles = Vector3.zero;
        unit.transform.localScale = Vector3.one;
        unit.gameObject.SetActive(false);

        return unit;
    }

    Unit UseUnit(UNIT_NAME unitName, TEAM team)
    {
        List<Unit> listUnits = mapUnitList_[unitName];
        if (listUnits.Count <= 0)
        {
            AddUnitPoolingList(unitName);
        }

        Unit unit = listUnits[0];
        listUnits.RemoveAt(0);
        listUsingUnits_.Add(unit);

        unit.transform.parent = unitAliveParentTransform_;
        unit.UnitPosition = Vector3.zero;
        unit.transform.localEulerAngles = Vector3.zero;
        unit.transform.localScale = Vector3.one;

        List<Renderer> listTeamColorRenderer = unit.TeamColorRenderers;
        for(int i = 0; i < listTeamColorRenderer.Count; ++i)
        {
            //Renderer renderer = listTeamColorRenderer[i];
            //renderer.material = mapTeamColorMaterial_[team];
            for(int m = 0; m < listTeamColorRenderer[i].materials.Length; ++m)
            {
                Material copyedMaterial = listTeamColorRenderer[i].materials[m];
                copyedMaterial.color = mapTeamColor_[team];
                listTeamColorRenderer[i].materials[m] = copyedMaterial;

            }
        }

        unit.gameObject.SetActive(true);

        return unit;
    }

    public Unit UseUnit(UnitData unitData)
    {
        Unit unit = UseUnit(unitData.UnitName, unitData.Team);
        unit.Init(unitData);

        return unit;
    }

    public Unit UseUnit(UnitData unitData, Vector3 worldPosition)
    {
        Unit unit = UseUnit(unitData.UnitName, unitData.Team);
        unit.WorldPosition = worldPosition;
        unit.Init(unitData);

        return unit;
    }

    public Unit UseUnit(UnitData unitData, UnitAttackData unitAttackData)
    {
        Unit unit = UseUnit(unitData.UnitName, unitData.Team);
        unit.Init(unitData, unitAttackData);

        return unit;
    }

    public Unit UseUnit(UnitData unitData, UnitAttackData unitAttackData, Vector3 worldPosition)
    {
        Unit unit = UseUnit(unitData.UnitName, unitData.Team);
        unit.WorldPosition = worldPosition;
        unit.Init(unitData, unitAttackData);

        return unit;
    }

    public void RestoreUnit(Unit unit)
    {
        UNIT_NAME unitName = unit.UnitName;
        unit.gameObject.SetActive(false);
        unit.transform.parent = unitPoolingParentTransform_;

        if (listUsingUnits_.Remove(unit))
        {
            mapUnitList_[unitName].Add(unit);
        }
    }

    // 매개변수 unit과 가장 가까운 팀이 다른 유닛 탐색
    public Unit FindNearEnemyUnitNullable(TEAM myTeam, Vector3 unitPosition)
    {
        float distance = float.MaxValue;
        Unit nearUnit = null;

        for (int i = 0; i < listUsingUnits_.Count; ++i)
        {
            Unit targetUnit = listUsingUnits_[i];

            if (myTeam == targetUnit.Team)
                continue;

            float newDistance = Vector3.Distance(unitPosition, targetUnit.UnitPosition);
            if (newDistance < distance)
            {
                distance = newDistance;
                nearUnit = targetUnit;
            }
        }

        return nearUnit;
    }

    public List<Unit> FindNearUnitListNullable(List<TEAM> targetTeam, Vector3 unitPosition, int cnt)
    {
        List<Unit> nearUnitList = new List<Unit>();

        List<float> distanceList = new List<float>();
        Dictionary<float, Unit> mapDistanceByUnit = new Dictionary<float, Unit>();

        for (int i = 0; i < listUsingUnits_.Count; ++i)
        {
            Unit targetUnit = listUsingUnits_[i];
                        
            if (targetTeam.Contains(targetUnit.Team) == false)
                continue;

            float distance = Vector3.Distance(unitPosition, targetUnit.UnitPosition);
            mapDistanceByUnit.Add(distance, targetUnit);
            distanceList.Add(distance);
        }

        if(cnt >= distanceList.Count)
        {
            for(int i = 0; i < distanceList.Count; ++i)
            {
                nearUnitList.Add(mapDistanceByUnit[distanceList[i]]);
            }
        }
        else
        {
            distanceList.Sort(); // 


            for (int i = 0; i < cnt; ++i)
            {
                nearUnitList.Add(mapDistanceByUnit[distanceList[i]]);
            }
        }
        
        return nearUnitList;
    }

    public void OnGameEnded()
    {
        for(int i = 0; i < listUsingUnits_.Count; ++i)
        {
            listUsingUnits_[i].OnGameEnded();
        }
    }
}
