using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Playable : Unit
{
    protected bool isCanFly_;
    protected float speed_ = 1.0f;

    public override void Init(UnitData unitData)
    {
        base.Init(unitData);

        isCanFly_ = false;
        rigidbody_.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        PlayableUnitUpdate();
    }

    protected virtual void PlayableUnitUpdate()
    {
        if (IsAlive == false)
            return;

        Transform unitModelTransform = cameraHolder_.GetUnitModule_AngleX_Transform().transform;
        if(isCanFly_)
        {
            unitModelTransform = cameraHolder_.GetUnitModuleTransform().transform;
        }

        // Move Input
        bool isMoveInputExist = false;

        if (Input.GetKey(KeyCode.W))
        {
            isMoveInputExist = true;
            AddVelocity(unitModelTransform.forward * speed_);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isMoveInputExist = true;
            AddVelocity(-unitModelTransform.forward * speed_);
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMoveInputExist = true;
            AddVelocity(-unitModelTransform.right * speed_);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            isMoveInputExist = true;
            AddVelocity(unitModelTransform.right * speed_);
        }
        if(isCanFly_)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isMoveInputExist = true;
                AddVelocity(unitModelTransform.up * speed_);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                isMoveInputExist = true;
                AddVelocity(-unitModelTransform.up * speed_);
            }
        }

        if (isMoveInputExist == false)
        {
            rigidbody_.velocity = Vector3.MoveTowards(rigidbody_.velocity, Vector3.zero, 30f * Time.smoothDeltaTime);
        }
    }

    void AddVelocity(Vector3 pos)
    {
        AddVelocity(pos.x, pos.y, pos.z);
    }

    void AddVelocity(float x, float y, float z)
    {
        float speed = unitData_.Speed;
        rigidbody_.AddForce(new Vector3(x, y, z) * speed * Time.smoothDeltaTime * 60f);
    }

    // 별도의 이동이 구현되어 있으므로 동작하지 않도록 오버라이딩
    protected override void Move(Vector3 targetUnitPosition) { }
}
