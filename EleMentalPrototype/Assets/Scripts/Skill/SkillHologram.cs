using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHologram : MonoBehaviour
{
    [Header("HologramMaterials")]
    [SerializeField] Material normalHologramMaterial_;
    [SerializeField] Material cannotHologramMaterial_;

    [Header("Common")]
    [SerializeField] List<Renderer> rendererList_ = new List<Renderer>();


    bool isNotOnTheGround_;
    public bool isNotOnTheGround
    {
        set { isNotOnTheGround_ = value; }
    }

    void Init()
    {

    }

    public void SetHologramEnable()
    {
        gameObject.SetActive(true);
        Init();
    }

    public void SetHologramDisable()
    {
        gameObject.SetActive(false);
    }
}
