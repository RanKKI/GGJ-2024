using System.Collections.Generic;
using Pixeye.Actors;
using UnityEngine;

public class ProcessorSound : Processor, IReceive<SignalPlaySound>
{
    protected AudioSource bgmSource;
    
    protected AudioSource effectSource;

    protected Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    protected bool isStop;

    public bool IsStop
    {
        get { return isStop; }
        set
        {
            isStop = value;
            if (isStop)
            {
                bgmSource?.Pause();
            }
            else
            {
                bgmSource?.Play();
            }
        }
    }

    # region Volume

    protected float bgmVolume;

    public float BgmVolume
    {
        get { return bgmVolume; }
        set
        {
            bgmVolume = value;
            bgmSource.volume = bgmVolume;
        }
    }

    protected float effectVolume;

    public float EffectVolume
    {
        get { return effectVolume; }
        set { effectVolume = value; }
    }

    # endregion

    public ProcessorSound()
    {
        bgmSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        effectSource = GameObject.Find("EffectMusic").GetComponent<AudioSource>();
        IsStop = false;
        BgmVolume = 1f;
        EffectVolume = 0.5f;
    }

    public void PlayBGM(string res)
    {
        if (isStop)
        {
            return;
        }

        if (clips.ContainsKey(res) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res, clip);
        }

        bgmSource.clip = clips[res];
        bgmSource.Play();
    }

    public void PlayEffect(string soundName, float volume, Vector3 pos)
    {
        if (isStop) return;
        AudioClip clip = null;
        if (!clips.ContainsKey(soundName))
        {
            clip = Resources.Load<AudioClip>($"Sounds/{soundName}");
            clips.Add(soundName, clip);
        }
        clip = clips[soundName];
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = pos;
        AudioSource audioSource = (AudioSource) gameObject.AddComponent(typeof (AudioSource));
        audioSource.clip = clip;
        audioSource.volume = volume * effectVolume;
        audioSource.outputAudioMixerGroup = effectSource.outputAudioMixerGroup;
        audioSource.Play();
        Object.Destroy((Object) gameObject, clip.length * ((double) UnityEngine.Time.timeScale < 0.009999999776482582 ? 0.01f : UnityEngine.Time.timeScale));
    }

    public void HandleSignal(in SignalPlaySound arg)
    {
        try{
            PlayEffect(arg.name, arg.volume, arg.pos);
        }catch(System.Exception e){
            Debug.Log(e);
        }
    }
}