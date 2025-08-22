using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePopup : Popup
{
    public override void Init()
    {
        base.Init();
    }

    public void OnRestartBtnClick()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        OnBackBtnClick();

        FadeManager.Instance.SceneChange(Context.GAME_SCENE);
    }

    public void OnResumeBtnClick()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        OnBackBtnClick();
    }

    public void OnHomeBtnClick()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        OnBackBtnClick();

        FadeManager.Instance.SceneChange(Context.LOBBY_SCENE);
    }

    public override void OnBackBtnClick()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        base.OnBackBtnClick();
    }
}
