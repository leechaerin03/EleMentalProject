using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub_Lobby : MonoBehaviour
{
    public static Hub_Lobby Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Application.targetFrameRate = 144;

        FadeManager.Instance.FadeOut();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SoundManager.Instance.SetBgm(BGM_STATUS.LOBBY);
    }
}
