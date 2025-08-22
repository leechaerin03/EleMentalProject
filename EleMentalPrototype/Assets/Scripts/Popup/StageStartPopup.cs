using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageStartPopup : Popup
{
    [SerializeField] Text stageNumText_;
    int stageNumber_;

    public void OnStageStartBtnClick()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        Context.g_selectChapter_ = 1; // 
        Context.g_selectStage_ = stageNumber_;

        PopupManager.Instance.CreatePopupAndShow(PopupName.Lobby_SelectElementPopup);
    }

    public void SetStageInfo(int stage)
    {
        stageNumber_ = stage;

        stageNumText_.text = stageNumber_.ToString();
    }
}
