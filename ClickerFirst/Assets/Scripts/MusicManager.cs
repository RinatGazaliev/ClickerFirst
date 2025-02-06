using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    
    public static MusicManager instance;
    public bool isSwapLocked=false;
    [SerializeField] AudioSource audioMusic;
    //[SerializeField] AudioSource audioMusicRun;
    private Coroutine playAudioCoroutine;

    public List<AudioClip> listBgMusic;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        //audioMusic = GetComponent<AudioSource>();

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void PlayMusicBG() {
        if (Config.isSound)
        {
            if (audioMusic.clip == null)
            {
                int k = Random.Range(0, listBgMusic.Count);
                Debug.Log("random play " + k);
                audioMusic.clip = listBgMusic[k];
                audioMusic.loop = true;
                audioMusic.Play();
            }
            else
            {
                audioMusic.Play();
            }
        }
    }
    
    public void PlayMusicBGFadeIn(bool isRunning, float fadeDuration) {
        Debug.Log("FadeInStarted");
        if (Config.isSound)
        {
            if (isRunning)
            {
                audioMusic.clip = listBgMusic[1];
                audioMusic.volume=0;
                audioMusic.Play();
                audioMusic.DOFade(0.1f, fadeDuration);
                Debug.Log("FadeMusicComplete");  
            }
            else
            {
                audioMusic.clip = listBgMusic[0];
                audioMusic.volume=0;
                audioMusic.Play();
                audioMusic.DOFade(0.1f, fadeDuration);
                Debug.Log("FadeMusicComplete");  
                
            }
           
            /*if (musicN==0)
            {
                PlaySound_Effect1();
                PlaySound_Effect1();
                PlaySound_Effect1();
            }
            else
            {
                PlaySound_Menu1();
                PlaySound_Menu2();
                PlaySound_Menu3();
            }*/
        }
    }
    
    public void SwapMusic(bool isRunning, float fadeDuration) {
        Debug.Log("FadeInStarted");
        if (Config.isSound&&!isSwapLocked)
        { 
            audioMusic.DOFade(0f, fadeDuration)
                .OnComplete(() => PlayMusicBGFadeIn(isRunning, fadeDuration));
        }
    }


    public void StopMusicBG() {
        audioMusic.Stop();
       // audioMusicRun.Stop();
        StopAudioLoop();
        
    }
    
    public void StopAudioLoop()
    {
        if (playAudioCoroutine != null)
        {
            StopCoroutine(playAudioCoroutine); // Останавливаем корутину
            playAudioCoroutine = null;
          //  audioMusicRun.Stop(); // Останавливаем текущий проигрываемый звук
        }
    }
    public void EnableMusic()
    {
        if (Config.isSound)
        {
            audioMusic.volume=0.1f;
        }
        //Config.SetMusic(true);
        
       // audioMusicRun.volume = 1;
    }
    
    public void DisableMusic()
    {
        audioMusic.DOComplete();
       // Config.SetMusic(false);
        
        audioMusic.volume=0;
     //   audioMusicRun.volume = 0;

    }
    
}
