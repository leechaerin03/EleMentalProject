using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUtils
{
    public static bool IsArangeIn(Unit unit, Unit targetUnit, float arange)
    {
        if (targetUnit == null)
            return false;
        arange = arange + UnitDefine.mapUnitHitArange_[targetUnit.UnitName];
        if (Vector3.Distance(unit.UnitPosition, targetUnit.UnitPosition) <= arange)
            return true;

        return false;
    }
}
