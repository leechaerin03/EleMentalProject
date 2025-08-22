using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM_STATUS
{
    LOBBY,
    NORMAL_STAGE,
    BOSS_STAGE,
}

public class SoundManager : MonoBehaviour
{
    public static string BGM_VOLUME_KEY = "BGMVolume";
    public static string SFX_VOLUME_KEY = "SFXVolume";

    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    [SerializeField] AudioSource bgmSource_;

    [Header("BGM Audio Sources")]
    [SerializeField] AudioClip lobbyBgmClip_;
    [SerializeField] AudioClip normalStageBgmClip_;
    [SerializeField] AudioClip bossBgmClip_;

    [Header("SFX Audio Sources")]
    [SerializeField] AudioClip hitNone_;
    [SerializeField] AudioClip hitMagic_;

    [SerializeField] AudioClip ButtonClick_;

    [SerializeField] AudioClip WinSound_;
    [SerializeField] AudioClip LoseSound_;

    [SerializeField] AudioClip DieSound_;

    [SerializeField] AudioClip tempestBlast_;
    [SerializeField] AudioClip aeroVortex_;
    [SerializeField] AudioClip chainLightning_;
    [SerializeField] AudioClip thunderResidue_;
    [SerializeField] AudioClip meteor_;
    [SerializeField] AudioClip fireRain_;
    [SerializeField] AudioClip blizzard_;
    [SerializeField] AudioClip glacialUprise_;



    Dictionary<string, AudioClip> mapAudioClips_ = new Dictionary<string, AudioClip>();

    [Header("Common")]
    [SerializeField] Transform soundEffectPoolingParentTransform_;
    [SerializeField] Transform soundEffectAliveParentTransform_;

    List<SoundEffect> listPoolingSoundEffects_ = new List<SoundEffect>();
    List<SoundEffect> listUsingSoundEffects_ = new List<SoundEffect>();

    int poolingCnt_ = 100;

    float bgmVolume_;
    public float BGMVolume
    {
        get { return bgmVolume_; }
    }
    float sfxVolume_;
    public float SFXVolume
    {
        get { return sfxVolume_; }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SoundEffectInit();
        VolumeInit();
    }

    void SoundEffectInit()
    {
        // 
        mapAudioClips_.Add(SoundName.Hit_None, hitNone_);
        mapAudioClips_.Add(SoundName.Hit_Magic, hitMagic_);

        mapAudioClips_.Add(SoundName.ButtonClick, ButtonClick_);

        mapAudioClips_.Add(SoundName.WinSound, WinSound_);
        mapAudioClips_.Add(SoundName.LoseSound, LoseSound_);

        mapAudioClips_.Add(SoundName.DieSound, DieSound_);


        mapAudioClips_.Add(SoundName.TempestBlast, tempestBlast_);
        mapAudioClips_.Add(SoundName.AeroVortex, aeroVortex_);
        mapAudioClips_.Add(SoundName.ChainLightning, chainLightning_);
        mapAudioClips_.Add(SoundName.ThunderResidue, thunderResidue_);
        mapAudioClips_.Add(SoundName.Meteor, meteor_);
        mapAudioClips_.Add(SoundName.FireRain, fireRain_);
        mapAudioClips_.Add(SoundName.Blizzard, blizzard_);
        mapAudioClips_.Add(SoundName.GlacialUprise, glacialUprise_);

    }

    public void SetBgm(BGM_STATUS status)
    {
        if(status == BGM_STATUS.LOBBY)
        {
            bgmSource_.clip = lobbyBgmClip_;
        }
        else if (status == BGM_STATUS.NORMAL_STAGE)
        {
            bgmSource_.clip = normalStageBgmClip_;
        }
        else if (status == BGM_STATUS.BOSS_STAGE)
        {
            bgmSource_.clip = bossBgmClip_;
        }

        bgmSource_.Play();
    }

    void AddEffectPoolingList()
    {
        for (int i = 0; i < poolingCnt_; ++i)
        {
            SoundEffect effect = CreateEffect();
            listPoolingSoundEffects_.Add(effect);
        }
    }

    SoundEffect CreateEffect()
    {
        SoundEffect effect;
        GameObject effectPrefab = Resources.Load("SoundEffect") as GameObject;

        effect = Instantiate(effectPrefab).GetComponent<SoundEffect>();
        
        effect.transform.parent = soundEffectPoolingParentTransform_;
        effect.transform.localPosition = Vector3.zero;
        effect.transform.localEulerAngles = Vector3.zero;
        effect.transform.localScale = Vector3.one;
        effect.gameObject.SetActive(false);

        return effect;
    }

    public SoundEffect UseSoundEffect(string soundEffectName)
    {
        if (listPoolingSoundEffects_.Count <= 0)
        {
            AddEffectPoolingList();
        }

        SoundEffect effect = listPoolingSoundEffects_[0];
        listPoolingSoundEffects_.RemoveAt(0);
        listUsingSoundEffects_.Add(effect);

        effect.transform.parent = soundEffectAliveParentTransform_;
        effect.transform.localPosition = Vector3.zero;
        effect.transform.localEulerAngles = Vector3.zero;
        effect.transform.localScale = Vector3.one;
        effect.gameObject.SetActive(true);
        effect.Init(mapAudioClips_[soundEffectName]);

        return effect;
    }

    public void RestoreSoundEffect(SoundEffect effect)
    {
        effect.gameObject.SetActive(false);
        effect.transform.parent = soundEffectPoolingParentTransform_;

        if (listUsingSoundEffects_.Remove(effect))
        {
            listPoolingSoundEffects_.Add(effect);
        }
    }

    void VolumeInit()
    {
        bgmVolume_ = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 0.5f);
        sfxVolume_ = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f);

        bgmSource_.volume = bgmVolume_;
    }

    public void SetBgmVolume(float volume)
    {
        bgmVolume_ = volume;
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        bgmSource_.volume = BGMVolume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume_ = volume;
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }
}
