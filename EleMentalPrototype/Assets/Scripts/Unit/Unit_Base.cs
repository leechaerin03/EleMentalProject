using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Base : Unit
{

    public override void Init(UnitData unitData)
    {
        base.Init(unitData);
        
        rigidbody_.constraints = RigidbodyConstraints.FreezeAll;
    }
}
