using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] Transform unitModule_;
    [SerializeField] Transform unitModule_AngleX_;

    UnitCamera camera_ = null;
    public UnitCamera UnitCamera
    {
        get { return camera_; }
    }

    bool isExistHoldCamera_ = false;

    public void SetCamera(UnitCamera camera)
    {
        camera_ = camera;
        camera_.OnCameraHolderChanged(this);

        isExistHoldCamera_ = true;
    }

    public void OffCameraNullable()
    {
        camera_ = null;
        isExistHoldCamera_ = false;
    }

    public Transform GetUnitModuleTransform()
    {
        return unitModule_;
    }

    public Transform GetUnitModule_AngleX_Transform()
    {
        return unitModule_AngleX_;
    }

    public bool IsExistHoldCamera()
    {
        return isExistHoldCamera_;
    }
}
