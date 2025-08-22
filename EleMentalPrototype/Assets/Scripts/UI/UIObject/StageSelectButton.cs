using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    enum StageSelectButtonStatus
    {
        COMPLETED,
        OPEN,
        LOCKED
    }

    [SerializeField] List<Text> stageNumTextList_ = new List<Text>();
    [SerializeField] GameObject stageCompletedIcon_;
    [SerializeField] GameObject stageOpenIcon_;
    [SerializeField] GameObject stageLockIcon_;

    int completeStage_ = 0;
    int stageNumber_ = 0;

    StageSelectButtonStatus buttonStatus_ = StageSelectButtonStatus.LOCKED;


    public void Awake()
    {
        completeStage_ = PlayerPrefs.GetInt(StageController.COMPLETED_STAGE_KEY, 0);
    }

    public void Init(int stageNum)
    {
        stageNumber_ = stageNum;
        //PopupManager.Instance.CreatePopupAndShow(PopupName.StageOpenPopup);

        if (stageNumber_ == completeStage_ + 1) //첫번째 맵인 경우 (첫번째 맵 Start)
        {
            stageCompletedIcon_.SetActive(false); //lobby layer 에서 받아와서 .SetActive 할거야
            stageOpenIcon_.SetActive(true);
            stageLockIcon_.SetActive(false);

            buttonStatus_ = StageSelectButtonStatus.OPEN;
        }
        else if (stageNumber_ < completeStage_ + 1) // 깰 맵의 이전일 경우 Complete 
        {
            stageCompletedIcon_.SetActive(true);
            stageOpenIcon_.SetActive(false);
            stageLockIcon_.SetActive(false);

            buttonStatus_ = StageSelectButtonStatus.COMPLETED;
        }
        else
        {
            stageCompletedIcon_.SetActive(false);
            stageOpenIcon_.SetActive(false);
            stageLockIcon_.SetActive(true);

            buttonStatus_ = StageSelectButtonStatus.LOCKED;
        }

        for(int i = 0; i < stageNumTextList_.Count; ++i)
        {
            stageNumTextList_[i].text = stageNumber_.ToString();
        }
        
    }

    public void OnStageIconClick()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        if (buttonStatus_ == StageSelectButtonStatus.COMPLETED ||
            buttonStatus_ == StageSelectButtonStatus.OPEN)
        {
            OnStageStartPopup(stageNumber_);
        }
        else
        {
            OnLockPopup(stageNumber_);
        }
    }

    public void OnStageStartPopup(int stage)
    {
        StageStartPopup popup = PopupManager.Instance.CreatePopupAndShow(PopupName.Lobby_StageOpenPopup) as StageStartPopup;
        popup.SetStageInfo(stage);
    }
    public void OnLockPopup(int stage)
    {
        StageStartPopup popup = PopupManager.Instance.CreatePopupAndShow(PopupName.Lobby_LockPopup) as StageStartPopup;
        popup.SetStageInfo(stage);
    }

}
