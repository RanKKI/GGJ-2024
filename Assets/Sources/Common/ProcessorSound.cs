using System.Collections.Generic;
using Pixeye.Actors;
using UnityEngine;

public class ProcessorSound : Processor, IReceive<SignalPlaySound>
{
    protected AudioSource bgmSource;

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
        IsStop = false;
        BgmVolume = 1f;
        EffectVolume = 2f;
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
        AudioSource.PlayClipAtPoint(clips[soundName], pos, volume * effectVolume);
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