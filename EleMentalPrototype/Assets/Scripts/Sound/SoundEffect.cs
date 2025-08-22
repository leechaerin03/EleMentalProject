using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] AudioSource audioSource_;
    [SerializeField] float playTime_;
    float elapsedTime_;

    public void Init(AudioClip clip)
    {
        audioSource_.clip = clip;
        elapsedTime_ = 0f;

        audioSource_.volume = SoundManager.Instance.SFXVolume;

        audioSource_.Play();
    }

    private void Update()
    {
        elapsedTime_ += Time.smoothDeltaTime;

        if(elapsedTime_ >= playTime_)
        {
            SoundManager.Instance.RestoreSoundEffect(this);
        }
    }
}
