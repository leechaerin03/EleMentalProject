using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageCompletePopup : Popup
{
    //[SerializeField] Text getCoinText_;
    [SerializeField] Text stageText_;

    bool isCleared_;

    public void SetIsCleared(bool value)
    {
        isCleared_ = value;


        if (isCleared_)
        {
            SoundManager.Instance.UseSoundEffect(SoundName.WinSound);
        }
        else
        {
            SoundManager.Instance.UseSoundEffect(SoundName.LoseSound);
        }

        stageText_.text = "STAGE " + Context.g_selectStage_.ToString();

        int getCoin = Random.Range(80, 120) + Context.g_selectStage_ * 10;
        //getCoinText_.text = getCoin.ToString();

        Context.Coin = Context.Coin + getCoin;
    }
    
    public void OnNextStageStartBtn()
    {
        if (isCleared_ == false)
            return;

        if (Context.g_selectStage_ >= 15)
            return;

        Context.g_selectStage_ += 1;

        SceneManager.LoadScene(Context.GAME_SCENE);
    }

    public void OnHomeBtn()
    {
        SceneManager.LoadScene(Context.LOBBY_SCENE);
    }

    public void OnReplayBtn()
    {
        SceneManager.LoadScene(Context.GAME_SCENE);
    }



}
