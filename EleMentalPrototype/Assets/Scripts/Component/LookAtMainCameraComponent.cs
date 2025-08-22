using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCameraComponent : MonoBehaviour
{
    Transform cameraTransform_;

    void Start()
    {
        cameraTransform_ = Hub_Ingame.Instance.cameraController_.MainCameraHolder.UnitCamera.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + cameraTransform_.rotation * Vector3.forward, cameraTransform_.rotation * Vector3.up);
    }
}
