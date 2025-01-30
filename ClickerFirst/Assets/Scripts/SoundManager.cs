using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSound;
    //  private Coroutine playAudioCoroutine;
    

    
    
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        audioSound = GetComponent<AudioSource>();

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Header("Butt Click")]
    public AudioClip butt_click;
    public void PlaySound_ButtClick()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(butt_click);
        }
    }
    
    [Header("Sosige Click")]
    public AudioClip sos_click;
    
    public void PlaySound_SosClick()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(sos_click);
        }
    }
    
    [Header("FlagWgt")]
    public AudioClip flag_wgt;
    
    public void PlaySound_FlagWgt()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(flag_wgt);
        }
    }
    
    [Header("PortalGroup")]
    public AudioClip kick;
    public AudioClip portal_appear;
    public AudioClip leg_appear;
    public void PlaySoundByName(string soundName)
    {
        AudioClip clip = null;
        switch (soundName)
        {
            case "KickAppear":
                if (Config.isSound)
                {
                    audioSound.PlayOneShot(leg_appear);
                }
                break;
            case "Punch":
                if (Config.isSound)
                {
                    audioSound.PlayOneShot(kick);
                }
                break;
            
            case "PortalAppear":
                if (Config.isSound)
                {
                    audioSound.PlayOneShot(portal_appear);
                }
                break;
            
            case "Ouch":
                if (Config.isSound)
                {
                    PlayRandomOuchSound();
                }
                break;
            // Добавь другие случаи
            default:
                Debug.LogWarning($"No clip found for sound: {soundName}");
                return;
        }

    }
    
    [Header("Ouch!")]
    public AudioClip ouch1;
    public AudioClip ouch2;
    public AudioClip ouch3;
    public AudioClip ouch4;
    public AudioClip ouch5;
    public void PlayRandomOuchSound()
    {
        if (audioSound == null)
        {
            Debug.LogWarning("AudioSource is missing!");
            return;
        }

        AudioClip[] ouchClips = { ouch1, ouch2, ouch3, ouch4, ouch5 };
        AudioClip randomClip = ouchClips[Random.Range(0, ouchClips.Length)];  // Выбор случайного клипа
        audioSound.PlayOneShot(randomClip);  // Проигрывание звука
    }
    public void DisableSound()
    {
        Config.SetSound(false);
        
        audioSound.volume=0; 

    }

    public void EnableSound()
    {
        Config.SetSound(true);
        audioSound.volume=1; 
    }
    
    public void StopAllSounds()
    {
        audioSound.Stop();
        //StopAudioLoop();
    }
}
