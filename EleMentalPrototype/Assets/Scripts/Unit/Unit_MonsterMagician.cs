using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_MonsterMagician : Unit_Attackable
{
    protected override void AttackEffectCreate()
    {
        base.AttackEffectCreate();

        Hub_Ingame.Instance.effectController_.UseEffect(EFFECT_NAME.PROJECTILE_MAGIC_NONE, UnitPosition, nearEnemyUnit_.transform, 0.8f, Effect_Projectile.PROJECTILE_TYPE.CURVE);

    }
}
