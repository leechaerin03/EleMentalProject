using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] protected float effectPlayTime_ = 1.0f; // Default Set
    [SerializeField] List<ActionInterface> actionList_ = new List<ActionInterface>();

    protected float elapsedTime_;
    bool isAlive_;

    EFFECT_NAME effectName_;
    public EFFECT_NAME EffectName
    {
        get { return effectName_; }
    }

    public virtual void Init(EFFECT_NAME effectName)
    {
        effectName_ = effectName;
        elapsedTime_ = 0f;
        isAlive_ = true;

        for(int i = 0; i < actionList_.Count; ++i)
        {
            actionList_[i].Init();
        }
    }

    public virtual void Init(EFFECT_NAME effectName, Vector3 startPosition, Transform targetTransform, float flyTime, Effect_Projectile.PROJECTILE_TYPE projectileType)
    {
        Init(effectName);
    }

    private void Update()
    {
        if (isAlive_ == false)
            return;

        EffectUpdate();
    }

    protected virtual void EffectUpdate()
    {
        elapsedTime_ += Time.smoothDeltaTime;

        if (elapsedTime_ >= effectPlayTime_)
        {
            isAlive_ = false;
            Hub_Ingame.Instance.effectController_.RestoreEffect(this);
        }
    }

    public Vector3 EffectPosition
    {
        get { return transform.localPosition; }
        set { transform.localPosition = value; }
    }

    public Vector3 WorldPosition
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
}
