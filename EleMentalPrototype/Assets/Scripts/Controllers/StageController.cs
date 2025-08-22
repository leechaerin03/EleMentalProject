using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GAME_STATUS
{
    NOT_STARTED,
    PLAYING,
    END
}

public class StageController : MonoBehaviour
{
    public static string COMPLETED_STAGE_KEY = "StageCompleteNum";

    [SerializeField] Transform fieldParent_;
    [SerializeField] List<Field> fieldList_ = new List<Field>();
    [SerializeField] List<Material> skyboxMaterialList_;

    GAME_STATUS gameStatus_ = GAME_STATUS.NOT_STARTED;
    Stage curStage_;
    Field curField_;
    public Field CurrentField
    {
        get { return curField_; }
    }

    int chapterNum_;
    int stageNum_;

    bool isGamePause_;

    private void Start()
    {
        isGamePause_ = false;
        Time.timeScale = 1;

        chapterNum_ = Context.g_selectChapter_;
        stageNum_ = Context.g_selectStage_;

        StageInit();
        FieldInit();
        SkyboxInit();
    }

    void StageInit()
    {
        gameStatus_ = GAME_STATUS.PLAYING;
    }

    void FieldInit()
    {
        if (fieldList_.Count <= 0)
            return;

        if(stageNum_ % 5 == 0)
        {
            curField_ = Instantiate(fieldList_[1]);
        }
        else
        {
            curField_ = Instantiate(fieldList_[0]);
        }
        curField_.transform.parent = fieldParent_;
        curField_.transform.localPosition = Vector3.zero;

        curStage_ = curField_.CurStage;
        curStage_.Init(stageNum_);

        if(stageNum_ % 5 == 0)
        {
            SoundManager.Instance.SetBgm(BGM_STATUS.BOSS_STAGE);
        }
        else
        {

            SoundManager.Instance.SetBgm(BGM_STATUS.NORMAL_STAGE);
        }
    }

    void SkyboxInit()
    {
        if (skyboxMaterialList_.Count != 5)
        {
            return;
        }
        RenderSettings.skybox = skyboxMaterialList_[(stageNum_ - 1) % 5];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PopupManager.Instance.IsAlivePopupExist == false)
            {
                PopupManager.Instance.CreatePopupAndShow(PopupName.Game_PausePopup);

            }
        }
    }

    public void BaseDestroy(TEAM destroyBaseTeam)
    {
        if (gameStatus_ != GAME_STATUS.PLAYING)
            return;

        bool isWin = destroyBaseTeam == TEAM.TEAM_2;

        if (isWin) // WIN
        {
            int completeStage = PlayerPrefs.GetInt(COMPLETED_STAGE_KEY);
            if(Context.Level < stageNum_)
            {
                PlayerPrefs.SetInt(COMPLETED_STAGE_KEY, stageNum_);
                Context.Level = stageNum_;
            }
        }
        else
        {

        }

        gameStatus_ = GAME_STATUS.END;

        // OnGameEnded
        curStage_.OnGameEnded();
        Hub_Ingame.Instance.unitController_.OnGameEnded();

        StartCoroutine(OnShowResultPopup(isWin));
    }

    IEnumerator OnShowResultPopup(bool isWin)
    {
        yield return new WaitForSeconds(1.0f);

        if(isWin)
        {
            StageCompletePopup popup = PopupManager.Instance.CreatePopupAndShow(PopupName.Game_StageCompletePopup) as StageCompletePopup;
            popup.SetIsCleared(isWin);
        }
        else
        {
            StageCompletePopup popup = PopupManager.Instance.CreatePopupAndShow(PopupName.Game_StageLosePopup) as StageCompletePopup;
            popup.SetIsCleared(isWin);
        }

    }
}
