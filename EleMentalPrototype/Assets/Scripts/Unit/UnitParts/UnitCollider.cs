using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollider : MonoBehaviour
{
    [SerializeField] Unit unit_;

    public Unit OwnUnit
    {
        get { return unit_; }
    }
}
