using UnityEngine.Audio;
using System;
using UnityEngine;
using DG.Tweening;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private Sound[] _sounds;

    private bool isMusicPlaying;
    private bool isSoundPlaying;
    private bool init = false;

    public void Init()
    {
        init = true;
        PlayerData playerData = SaveSystemManager.GetInstance().GetPlayerData();
        isMusicPlaying = playerData._BGMOn;
        isSoundPlaying = playerData._SFXOn;

        //PlayAudio("bgm");
    }

    private void Start()
    {
        TryInit();
    }

    private void TryInit() 
    {
        if (!init)
            Init();
    }

    ///<summary>
    ///This creates an audio source GameObject parented to the
    ///AudioManager's transform and plays the sound.
    ///</summary>
    public Sound PlayAudio(string name)
    {
        TryInit();
        Sound s = GetSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return null;
        }

        Debug.Log("Playing " + s.name);

        if (s.source == null)
            SetAudioSource(s);

        if ((s.IsBGM && !IsMusicPlaying()) || (!s.IsBGM && !IsSoundPlaying()))
            s.source.mute = true;
        else
            s.source.mute = false;

        s.Play();
        return s;
    }

    ///<summary>
    ///This receives a GameObject which is used as the audio source to give
    ///a more 3D-ish effect to the sound that is going to be played.
    ///</summary>
    public Sound PlayAudio(string name, GameObject audioSource)
    {
        TryInit();
        Sound s = GetSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return null;
        }

        Debug.Log("Playing " + s.name);

        SetAudioSource(s, audioSource);

        if ((s.IsBGM && !IsMusicPlaying()) || (!s.IsBGM && !IsSoundPlaying()))
            s.source.mute = true;
        else
            s.source.mute = false;

        s.source.Play();
        return s;
    }

    public Sound GetSound(string name) => Array.Find(_sounds, sound => sound.name == name);

    public bool ToggleMusic(bool value)
    {
        TryInit();
        isMusicPlaying = value;
        var musics = Array.FindAll(_sounds, sound => sound.IsBGM == true);
        foreach (var music in musics)
        {
            if (music.source != null)
                music.source.mute = !isMusicPlaying;
        }
        SaveMusicOn();
        return this.isMusicPlaying;
    }

    public bool IsMusicPlaying() => isMusicPlaying;

    public bool IsMusicPlaying(string name)
    {
        foreach (Sound s in this._sounds)
        {
            if (s.IsBGM && s.name.Equals(name) && s.source && s.source.isPlaying)
                return true;
        }
        return false;
    }
    public bool IsSoundPlaying(string name)
    {
        foreach (Sound s in this._sounds)
        {
            if (!s.IsBGM && s.name.Equals(name) && s.source && s.source.isPlaying)
                return true;
        }
        return false;
    }

    public void StopPlayingFadeOut(string name) 
    {
        foreach (Sound s in this._sounds) 
        {
            if (s.source && s.name.Equals(name) && s.source.isPlaying) 
            {
                s.source.DOFade(0f,.4f);
            }
        }
    }
    
    public void SwitchBGM(string newBgm, bool smoothTransition = false)
    {
        var bgms = Array.FindAll(_sounds, sound => sound.IsBGM == true);
        var isPlaying = false;

        if (smoothTransition)
        {
            foreach (var bgm in bgms)
            {
                if (bgm.source != null && !(bgm.name == newBgm))
                {
                    DOTween.To(() => bgm.source.volume, value => bgm.source.volume = value, 0f, 2f)
                        .OnComplete(() => Destroy(bgm.source.gameObject));
                }
                else
                {
                    if (bgm.name == newBgm)
                    {
                        if (bgm.source != null && bgm.source.isPlaying)
                            isPlaying = true;
                    }
                }

            }
            if (!isPlaying)
            {
                var sound = PlayAudio(newBgm);
                sound.source.volume = 0f;
                DOTween.To(() => sound.source.volume, value => sound.source.volume = value, sound.volume, 1.5f);
            }
        }
        else
        {
            foreach (var bgm in bgms)
            {
                if (bgm.source != null && !(bgm.name == newBgm))
                    Destroy(bgm.source.gameObject);
                else
                {
                    if (bgm.name == newBgm)
                    {
                        if (bgm.source != null && bgm.source.isPlaying)
                            isPlaying = true;
                    }
                }

            }
            if (!isPlaying)
                PlayAudio(newBgm);
        }
    }

    private void SaveMusicOn()
    {
        var playerData = SaveSystemManager.GetInstance().GetPlayerData();
        playerData._BGMOn = isMusicPlaying;
        SaveSystemManager.GetInstance().Save();
    }

    public bool ToggleSounds(bool value)
    {
        TryInit();
        isSoundPlaying = value;
        var sounds = Array.FindAll(_sounds, sound => sound.IsBGM == false);
        foreach (var sound in sounds)
        {
            if (sound.source != null)
                sound.source.mute = !isSoundPlaying;
        }
        SaveSoundOn();
        return this.isSoundPlaying;
    }

    public bool IsSoundPlaying() => isSoundPlaying;

    private void SaveSoundOn()
    {
        var playerData = SaveSystemManager.GetInstance().GetPlayerData();
        playerData._SFXOn = isSoundPlaying;
        SaveSystemManager.GetInstance().Save();
    }

    private void SetAudioSource(Sound s)
    {
        var go = new GameObject(s.name + "_Sound");
        go.transform.SetParent(transform);
        var source = go.AddComponent<AudioSource>();

        if (source == null) return;

        s.source = source;
        SetSound(s);
    }

    private void SetAudioSource(Sound s, GameObject audioSource)
    {
        var objectSource = audioSource.GetComponent<AudioSource>();
        AudioSource source;
        if (objectSource == null)
            source = audioSource.AddComponent<AudioSource>();
        else
            source = objectSource;

        s.source = source;
        SetSound(s);
    }

    private void SetSound(Sound s)
    {
        s.source.clip = s.audioFile;
        s.source.pitch = s.pitch;
        s.source.volume = s.volume;
        s.source.loop = s.loop;
    }
}
