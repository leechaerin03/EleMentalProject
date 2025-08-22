using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub_Ingame : MonoBehaviour
{
    public static Hub_Ingame Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Application.targetFrameRate = 144;//

        FadeManager.Instance.FadeOut();
    }

    public CameraController cameraController_;
    public UnitController unitController_;
    public StageController stageController_;
    public StageUIController stageUIController_;
    public EffectController effectController_;
    public SkillController skillController_;

}
