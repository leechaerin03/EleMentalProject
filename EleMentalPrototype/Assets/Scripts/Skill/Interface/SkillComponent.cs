using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillComponent : MonoBehaviour
{
    protected  List<Unit> targetUnitList_ = new List<Unit>();
    
    public void SetTargetUnitList(List<Unit> targetUnitList)
    {
        targetUnitList_ = targetUnitList;
    }

    public virtual void PlayComponentAction()
    {

    }
}
