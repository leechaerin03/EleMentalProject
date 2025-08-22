using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCamera : MonoBehaviour
{
    [SerializeField] Camera camera_;

    bool isHolderChangePositionLerping_;
    bool isHolderChangeAngleLerping_;
    Matrix4x4 origiProjectionMatrix_;

    private void Start()
    {
        origiProjectionMatrix_ = camera_.projectionMatrix;
    }

    private void CameraShake()
    {
        Matrix4x4 mat = origiProjectionMatrix_;

        // roll
        mat.m01 += Mathf.Sin(Time.time * 2.5f) * 0.15f; // 1.25
        mat.m10 += Mathf.Sin(Time.time * 2.5f) * 0.15f; // 1.5

        // x
        mat.m00 += Mathf.Sin(Time.time * 2.5f) * 0.075f; // 1.5
        mat.m11 += Mathf.Sin(Time.time * 2.5f) * 0.075f; // 1.5

        camera_.projectionMatrix = mat;
    }

    private void Update()
    {
        float time = Mathf.Max(Time.smoothDeltaTime / (1f / Application.targetFrameRate), 1f);
        //time = Mathf.Pow(time, 2);

        if (isHolderChangePositionLerping_)
        {
            CameraShake(); // localPosition의 최초 값을 기준으로 0 ~ 1, 2 로 하는쪽으로 구상 중
            
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 0.02f * time);
            if(Vector3.Magnitude(transform.localPosition) <= 0.5f)
            {
                transform.localPosition = Vector3.zero;
                isHolderChangePositionLerping_ = false;

                camera_.projectionMatrix = origiProjectionMatrix_;
            }
        }

        if(isHolderChangeAngleLerping_)
        {
            Vector3 currentAngle = transform.localEulerAngles;

            currentAngle = new Vector3 (
                Mathf.LerpAngle(currentAngle.x, 0, 0.08f * time),
                Mathf.LerpAngle(currentAngle.y, 0, 0.08f * time),
                Mathf.LerpAngle(currentAngle.z, 0, 0.08f * time));

            transform.localEulerAngles = currentAngle;

            if(Vector3.Magnitude(transform.localEulerAngles) <= 8f)
            {
                transform.localEulerAngles = Vector3.zero;
                isHolderChangeAngleLerping_ = false;
            }
        }
    }

    public void OnCameraHolderChanged(CameraHolder cameraHolder)
    {
        transform.parent = cameraHolder.transform;
        
        isHolderChangePositionLerping_ = true;
        isHolderChangeAngleLerping_ = true;
    }
}
