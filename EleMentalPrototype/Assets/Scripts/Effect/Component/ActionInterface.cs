using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInterface : MonoBehaviour
{
    [SerializeField] protected float startDelayTime_ = 0f;
    [SerializeField] protected float durationTime_ = 0.5f;
    protected float elapsedTime_;
    protected bool isAlive_;

    public virtual void Init()
    {
        elapsedTime_ = -startDelayTime_;
        isAlive_ = true;
    }

    private void Update()
    {
        if (isAlive_ == false)
            return;

        ActionUpdate();
    }

    protected virtual void ActionUpdate()
    {
        elapsedTime_ += Time.smoothDeltaTime;

        if(elapsedTime_ >= durationTime_)
        {
            isAlive_ = false;
        }
    }
}
