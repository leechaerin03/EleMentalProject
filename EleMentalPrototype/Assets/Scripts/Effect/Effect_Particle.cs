using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Particle : Effect
{
    [SerializeField] ParticleSystem particle_;
    
    public override void Init(EFFECT_NAME effectName)
    {
        base.Init(effectName);

        particle_.Play();
    }
}
