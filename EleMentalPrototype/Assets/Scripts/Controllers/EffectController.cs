using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EffectController : MonoBehaviour
{
    [SerializeField] Transform effectPoolingParentTransform_;
    [SerializeField] Transform effectAliveParentTransform_;


    Dictionary<EFFECT_NAME, string> mapEffectPrefabName_ = new Dictionary<EFFECT_NAME, string>();

    readonly Dictionary<EFFECT_NAME, int> mapEffectPoolingCnt_ = EffectDefine.mapEffectPoolingCnt_;

    Dictionary<EFFECT_NAME, List<Effect>> mapEffectList_;
    List<Effect> listUsingEffects_ = new List<Effect>();

    private void Awake()
    {
        FactoryInit();
        UnitNameInit();

    }

    void FactoryInit()
    {
        mapEffectList_ = new Dictionary<EFFECT_NAME, List<Effect>>();

        // 
        mapEffectList_.Add(EFFECT_NAME.DAMAGE_NUMBER, new List<Effect>());

        mapEffectList_.Add(EFFECT_NAME.UNIT_DEAD_EFFECT, new List<Effect>());

        mapEffectList_.Add(EFFECT_NAME.HIT_NONE_ELEMENT, new List<Effect>());
        mapEffectList_.Add(EFFECT_NAME.HIT_FIRE_ELEMENT, new List<Effect>());
        mapEffectList_.Add(EFFECT_NAME.HIT_ICE_ELEMENT, new List<Effect>());
        mapEffectList_.Add(EFFECT_NAME.HIT_AIR_ELEMENT, new List<Effect>());
        mapEffectList_.Add(EFFECT_NAME.HIT_ELECTRICITY_ELEMENT, new List<Effect>());

        mapEffectList_.Add(EFFECT_NAME.ICE_CRISTAL, new List<Effect>());

        mapEffectList_.Add(EFFECT_NAME.PROJECTILE_ARROW, new List<Effect>());
        mapEffectList_.Add(EFFECT_NAME.PROJECTILE_MAGIC_NONE, new List<Effect>());
        mapEffectList_.Add(EFFECT_NAME.PROJECTILE_MAGIC_FIRERAIN, new List<Effect>());
        mapEffectList_.Add(EFFECT_NAME.PROJECTILE_MAGIC_THUNDER, new List<Effect>());

        mapEffectList_.Add(EFFECT_NAME.CIRCLE_REFRECTION, new List<Effect>());
        mapEffectList_.Add(EFFECT_NAME.CIRCLE_REFRECTION_FROM_CAMERA, new List<Effect>());
    }

    void UnitNameInit()
    {
        mapEffectPrefabName_.Add(EFFECT_NAME.DAMAGE_NUMBER, EffectPrefabName.Damage);

        mapEffectPrefabName_.Add(EFFECT_NAME.UNIT_DEAD_EFFECT, EffectPrefabName.UnitDeadEffect);

        mapEffectPrefabName_.Add(EFFECT_NAME.HIT_NONE_ELEMENT, EffectPrefabName.HitEffect_NONE);
        mapEffectPrefabName_.Add(EFFECT_NAME.HIT_FIRE_ELEMENT, EffectPrefabName.HitEffect_FIRE);
        mapEffectPrefabName_.Add(EFFECT_NAME.HIT_ICE_ELEMENT, EffectPrefabName.HitEffect_ICE);
        mapEffectPrefabName_.Add(EFFECT_NAME.HIT_AIR_ELEMENT, EffectPrefabName.HitEffect_AIR);
        mapEffectPrefabName_.Add(EFFECT_NAME.HIT_ELECTRICITY_ELEMENT, EffectPrefabName.HitEffect_ELECTRICITY);

        mapEffectPrefabName_.Add(EFFECT_NAME.ICE_CRISTAL, EffectPrefabName.Ice_Cristal);

        mapEffectPrefabName_.Add(EFFECT_NAME.PROJECTILE_ARROW, EffectPrefabName.Projectile_Arrow);
        mapEffectPrefabName_.Add(EFFECT_NAME.PROJECTILE_MAGIC_NONE, EffectPrefabName.Projectile_Magic_None);
        mapEffectPrefabName_.Add(EFFECT_NAME.PROJECTILE_MAGIC_FIRERAIN, EffectPrefabName.Projectile_Magic_FireRain);
        mapEffectPrefabName_.Add(EFFECT_NAME.PROJECTILE_MAGIC_THUNDER, EffectPrefabName.Projectile_Magic_Thunder);

        mapEffectPrefabName_.Add(EFFECT_NAME.CIRCLE_REFRECTION, EffectPrefabName.CircleRefrection);
        mapEffectPrefabName_.Add(EFFECT_NAME.CIRCLE_REFRECTION_FROM_CAMERA, EffectPrefabName.CircleRefrectionFromCamera);

    }

    void AddEffectPoolingList(EFFECT_NAME effectName)
    {
        if (effectName == EFFECT_NAME.NONE)
        {
            Assert.IsTrue(true);
            return;
        }

        List<Effect> listEffect = mapEffectList_[effectName];
        int cnt = mapEffectPoolingCnt_[effectName];

        for (int i = 0; i < cnt; ++i)
        {
            Effect effect = CreateEffect(effectName);
            listEffect.Add(effect);
        }
    }

    Effect CreateEffect(EFFECT_NAME effectName)
    {
        Effect effect;

        string prefabName;
        if (mapEffectPrefabName_.TryGetValue(effectName, out prefabName) == false)
        {
            effect = new Effect();
            Assert.IsTrue(true);
        }
        else
        {
            GameObject effectPrefab = Resources.Load(prefabName) as GameObject;

            effect = Instantiate(effectPrefab).GetComponent<Effect>();
        }

        effect.transform.parent = effectPoolingParentTransform_;
        effect.EffectPosition = Vector3.zero;
        effect.transform.localEulerAngles = Vector3.zero;
        effect.transform.localScale = Vector3.one;
        effect.gameObject.SetActive(false);

        return effect;
    }

    public Effect UseEffect(EFFECT_NAME effectName)
    {
        List<Effect> listEffects = mapEffectList_[effectName];
        if (listEffects.Count <= 0)
        {
            AddEffectPoolingList(effectName);
        }

        Effect effect = listEffects[0];
        listEffects.RemoveAt(0);
        listUsingEffects_.Add(effect);

        effect.transform.parent = effectAliveParentTransform_;
        effect.EffectPosition = Vector3.zero;
        effect.transform.localEulerAngles = Vector3.zero;
        effect.transform.localScale = Vector3.one;
        effect.gameObject.SetActive(true);
        effect.Init(effectName);

        return effect;
    }

    public Effect UseEffect(EFFECT_NAME effectName, Vector3 effectPosition)
    {
        Effect effect = UseEffect(effectName);
        effect.EffectPosition = effectPosition;

        return effect;
    }

    public Effect UseEffect(EFFECT_NAME effectName, Unit targetUnit)
    {
        Vector3 unitHitPosition = targetUnit.UnitPosition + new Vector3(0, UnitDefine.mapUnitHitYPosition_[targetUnit.UnitName], 0);
        return UseEffect(effectName, unitHitPosition);
    }

    public Effect UseEffect(EFFECT_NAME effectName, Vector3 startPosition, Transform targetTransform, float flyTime, Effect_Projectile.PROJECTILE_TYPE projectileType)
    {
        Effect effect = UseEffect(effectName);
        effect.Init(effectName, startPosition, targetTransform, flyTime, projectileType);

        return effect;
    }
    
    public void RestoreEffect(Effect effect)
    {
        EFFECT_NAME effectName = effect.EffectName;
        effect.gameObject.SetActive(false);
        effect.transform.parent = effectPoolingParentTransform_;

        if (listUsingEffects_.Remove(effect))
        {
            mapEffectList_[effectName].Add(effect);
        }
    }
}
