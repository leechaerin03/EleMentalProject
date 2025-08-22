using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] UnitCamera mainCamera_;
    CameraHolder mainCameraHolder_ = null;


    float cameraXAxisRotateSpeed_ = 5; //카메라 x축 회전속도
    float cameraYAxisRotateSpeed_ = 3; //카메라 Y축 회전속도
    float cameraAngleX;
    float cameraAngleY;

    bool isCameraRotateableAtMouse_;
    bool isPopupAlive_;
    public bool IsPopupAlive
    {
        set { isPopupAlive_ = value; }
    }

    float cameraEnterTeamUnitTime_;
    bool isCameraEnterTeamUnit_;
    Action cameraEnterEndCallback_;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PopupManager.Instance.IsLockedCursor = true;

        cameraAngleX = 0f;
        cameraAngleY = 0f;
        isCameraRotateableAtMouse_ = true;

        isCameraEnterTeamUnit_ = false;
    }

    private void Update()
    {
        if(isCameraRotateableAtMouse_ && 
            isPopupAlive_ == false)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            CameraRotateUpdate(mouseX, mouseY);
        }

        if(isCameraEnterTeamUnit_)
        {
            cameraEnterTeamUnitTime_ -= Time.smoothDeltaTime;
            if(cameraEnterTeamUnitTime_ <= 0f ||
                mainCameraHolder_.isActiveAndEnabled == false)
            {
                isCameraEnterTeamUnit_ = false;
                cameraEnterEndCallback_?.Invoke();
            }
        }
    }

    void CameraRotateUpdate(float mouseX, float mouseY)
    {
        int isMouseReverse = 1;

        cameraAngleY -= mouseY * cameraYAxisRotateSpeed_ * isMouseReverse;
        cameraAngleX += mouseX * cameraXAxisRotateSpeed_ * isMouseReverse;
        
        cameraAngleY = ClampAngle(cameraAngleY, -90, 90);

        if (mainCameraHolder_ == null ||
            mainCameraHolder_.GetUnitModuleTransform() == null ||
            mainCameraHolder_.GetUnitModule_AngleX_Transform() == null)
            return;
        
        mainCameraHolder_.GetUnitModule_AngleX_Transform().localRotation = Quaternion.Euler(0, cameraAngleX, 0);
        mainCameraHolder_.GetUnitModuleTransform().localRotation = Quaternion.Euler(cameraAngleY, 0, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    public void SetCameraHolding(CameraHolder cameraHolder, bool resetAngle = true)
    {
        if(mainCameraHolder_ != null &&
           mainCameraHolder_.IsExistHoldCamera())
        {
            mainCameraHolder_.OffCameraNullable();
        }

        if(resetAngle && mainCameraHolder_ != null)
        {
            mainCameraHolder_.GetUnitModule_AngleX_Transform().localEulerAngles = Vector3.zero;
            mainCameraHolder_.GetUnitModuleTransform().localEulerAngles = Vector3.zero;
        }

        cameraHolder.SetCamera(mainCamera_);
        mainCameraHolder_ = cameraHolder;

        cameraAngleX = mainCameraHolder_.GetUnitModule_AngleX_Transform().localEulerAngles.y;
        cameraAngleY = mainCameraHolder_.GetUnitModuleTransform().localEulerAngles.x;
    }

    public void CameraEnterTeamUnit(Unit targetUnit, float enterTime, Action cameraEnterEndCallback)
    {
        cameraEnterTeamUnitTime_ = enterTime;
        isCameraEnterTeamUnit_ = true;
        cameraEnterEndCallback_ = cameraEnterEndCallback;

        SetCameraHolding(targetUnit.CameraHolder, false); // 창조자에서 다른 유닛으로 들어가므로 각도 초기화 하지 않음
    }

    public CameraHolder MainCameraHolder
    {
        get { return mainCameraHolder_; }
    }
}
