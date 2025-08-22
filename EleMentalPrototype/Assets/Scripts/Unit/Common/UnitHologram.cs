using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHologram : MonoBehaviour
{
    [Header("HologramMaterials")]
    [SerializeField] Material normalHologramMaterial_;
    [SerializeField] Material cannotHologramMaterial_;

    [Header("Common")]
    [SerializeField] List<Renderer> rendererList_ = new List<Renderer>();

    int collisionCnt_;
    
    bool isCollisionUnit_;
    public bool isCollisionUnit
    {
        get { return isCollisionUnit_; }
    }

    bool isNotOnTheGround_;
    public bool isNotOnTheGround
    {
        set { isNotOnTheGround_ = value; }
    }

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        collisionCnt_ = 0;
        isCollisionUnit_ = false;
    }

    private void Update()
    {
        SetHologramMaterial();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            collisionCnt_ += 1;
            
            isCollisionUnit_ = collisionCnt_ > 0;
        }

        SetHologramMaterial();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            collisionCnt_ -= 1;

            isCollisionUnit_ = collisionCnt_ > 0;
        }

        SetHologramMaterial();
    }

    void SetHologramMaterial()
    {
        if (isCollisionUnit_ || isNotOnTheGround_)
        {
            for (int i = 0; i < rendererList_.Count; ++i)
            {
                rendererList_[i].material = cannotHologramMaterial_;
            }
        }
        else
        {
            for (int i = 0; i < rendererList_.Count; ++i)
            {
                rendererList_[i].material = normalHologramMaterial_;
            }
        }
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
