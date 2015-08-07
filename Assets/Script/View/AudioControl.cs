using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {

    public GameObject bgmObj;
    public GameObject sfxObj;
    public GameObject voiceObj;

    AudioSource bgm;
    AudioSource sfx;
    AudioSource voice;

	// Use this for initialization
	void Start ()
    {
        bgm = bgmObj.GetComponent<AudioSource>();
        sfx = sfxObj.GetComponent<AudioSource>();
        voice = voiceObj.GetComponent<AudioSource>();
	
	}

    public void PlayBGM(string musicName, float volume = 1)
    {
        bgm.volume = volume;
        bgm.clip = ResourcesOperator.LoadAudioClip("snd/mus/" + musicName);
        bgm.Play();
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void PlaySFX(string sfxName, float volume = 1)
    {
        sfx.volume = volume;
        sfx.clip = ResourcesOperator.LoadAudioClip("snd/sfx/" + sfxName);
        sfx.Play();
    }

    public void StopSFX()
    {
        sfx.Stop();
    }

    public void PlayVoice(string voiceName, float volume = 1)
    {
        voice.volume = volume;
        voice.clip = ResourcesOperator.LoadAudioClip(G.voicePath + voiceName);
        voice.Play();
    }

    public void StopVoice()
    {
        voice.Stop();
    }

	// Update is called once per frame
	void Update () 
    {
	
	}
}
