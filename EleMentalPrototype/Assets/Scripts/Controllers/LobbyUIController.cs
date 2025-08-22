using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LobbyUIController : MonoBehaviour
{
    [SerializeField] GameObject lobbyLayer_;
    [SerializeField] GameObject chapterLayer_;
    [SerializeField] GameObject stageSelectLayer_;

    [SerializeField] GameObject backBtnObj_;

    [SerializeField] Text coinText_;

    List<StageSelectButton> stageSelectButtonList_ = new List<StageSelectButton>();
    
    public void OnBackBtnClick()
    {
        backBtnObj_.SetActive(false);
        lobbyLayer_.SetActive(true);
        chapterLayer_.SetActive(false);
        stageSelectLayer_.SetActive(false);
    }

    public void OnStartBtnClick()
    {
        backBtnObj_.SetActive(true);
        lobbyLayer_.SetActive(false);
        chapterLayer_.SetActive(true);
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
    }

    public void OnChapterBtnClick()
    {
        backBtnObj_.SetActive(true);
        Context.g_IsPracticeMode_ = false;
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        chapterLayer_.SetActive(false);
        stageSelectLayer_.SetActive(true);
        GameObject creatStageSelectButton = Resources.Load("StageSelectButton") as GameObject; //Stage 프리팹 가져오기

        if (creatStageSelectButton == null)
        {
            Debug.LogError("CreatStageSelect is not exist");
            return;
        }

        if(stageSelectButtonList_.Count > 0)
        {
            for (int i = 0; i < 15; i++)
            {
                StageSelectButton stageSelectButton = stageSelectButtonList_[i];
                stageSelectButton.Init(i + 1);
            }
        }
        else
        {
            for (int i = 0; i < 15; i++)
            {
                GameObject newStageIcon = Instantiate(creatStageSelectButton);
                newStageIcon.transform.parent = stageSelectLayer_.transform;

                StageSelectButton stageSelectButton = newStageIcon.GetComponent<StageSelectButton>();
                stageSelectButton.Init(i + 1);

                stageSelectButtonList_.Add(stageSelectButton);
            }
        }
    }

    public void OnPracticeBtnClick()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        Context.g_IsPracticeMode_ = true;
        Context.g_selectStage_ = 1;

        Context.selectElement_ = ELEMENT.FIRE;

        FadeManager.Instance.SceneChange(Context.GAME_SCENE);

    }

    private void Update()
    {
        coinText_.text =  Context.Coin.ToString("#,##0");
    }

    public void OnInventoryPopup()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        Popup Popup = PopupManager.Instance.CreatePopupAndShow(PopupName.Lobby_InventoryPopup); 

        InventoryPopup inventoryPopup = Popup.GetComponent<InventoryPopup>();
        inventoryPopup.CreateSkillIventoryBox();

    }

    public void OnOptionPopup()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        PopupManager.Instance.CreatePopupAndShow(PopupName.Lobby_OptionPopup);

    }

    public void OnCoinPopup()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        //PopupManager.Instance.CreatePopupAndShow(PopupName.LockPopup);

    }

    public void OnHeartPopup()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        //PopupManager.Instance.CreatePopupAndShow(PopupName.LockPopup);

    }

    public void OnDiaPopup()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        //PopupManager.Instance.CreatePopupAndShow(PopupName.LockPopup);

    }

}
