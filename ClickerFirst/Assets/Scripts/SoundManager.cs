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
