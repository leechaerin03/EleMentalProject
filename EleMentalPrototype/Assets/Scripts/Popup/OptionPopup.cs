using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionPopup : Popup
{
    [SerializeField] GameObject noticeOnObj_;
    [SerializeField] GameObject noticeOffObj_;
    [SerializeField] Slider bgmSlider_;
    [SerializeField] Slider sfxSlider_;
    // [SerializeField] Text bgmText_;
    // [SerializeField] Text sfxText_;

    // [SerializeField] Toggle NoticeToggle;

    bool isNoticeOn_ = true;

    public override void Init()
    {
        base.Init();

        bgmSlider_.value = SoundManager.Instance.BGMVolume;
        sfxSlider_.value = SoundManager.Instance.SFXVolume;

        // bgmText_.text = bgmSlider_.value.ToString("F");
        // sfxText_.text = sfxSlider_.value.ToString("F");
    }
    
    void Start()
    {
        bgmSlider_.onValueChanged.AddListener(OnBGMValueChanged);
        sfxSlider_.onValueChanged.AddListener(OnSFXValueChanged);
    }

    public void OnBGMValueChanged(float volume)
    {
        //bgmText_.text = volume.ToString("F");//소수점 2자리 까지 Text에 출력하기

        SoundManager.Instance.SetBgmVolume(volume);
    }
    public void OnSFXValueChanged(float volume)
    {
        //sfxText_.text = volume.ToString("F");//소수점 2자리 까지 Text에 출력하기

        SoundManager.Instance.SetSfxVolume(volume);
    }

    public void OnNoticeToggleClick()
    {
        SoundManager.Instance.UseSoundEffect(SoundName.ButtonClick);
        isNoticeOn_ = !isNoticeOn_;
        noticeOnObj_.SetActive(isNoticeOn_);
        noticeOffObj_.SetActive(!isNoticeOn_);
    }

}
