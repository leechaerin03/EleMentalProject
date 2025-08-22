using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHPGauge : MonoBehaviour
{
    [SerializeField] GameObject hpBar_;

    void Start()
    {
        
    }

    public void HpGaugeUpdate(int hp, int maxHp)
    {
        hpBar_.transform.localScale = new Vector3(((float)hp / (float)maxHp), hpBar_.transform.localScale.y, hpBar_.transform.localScale.z);
    }
}
