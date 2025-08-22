using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Damage : Effect
{
    [SerializeField] TextMesh textMesh_;
    [SerializeField] Transform randomPositionTransform_;

    public void SetDamageText(int damage)
    {
        if (textMesh_ == null)
            return;

        randomPositionTransform_.localPosition = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));

        textMesh_.text = damage.ToString();
    }
}
