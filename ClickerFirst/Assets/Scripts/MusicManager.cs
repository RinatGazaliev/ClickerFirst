using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    
    public static MusicManager instance;
    [SerializeField] AudioSource audioMusic;
    [SerializeField] AudioSource audioMusicRun;
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
        if (Config.isMusic)
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
    
    public void PlayMusicBGFadeIn(int musicN, float fadeDuratyion) {
        Debug.Log("FadeInStarted");
        if (Config.isMusic)
        {
            if (musicN==0)
            {
                audioMusic.clip = listBgMusic[musicN];
                audioMusic.volume=0;
                audioMusic.Play();
                audioMusic.DOFade(0.35f, fadeDuratyion);
                Debug.Log("FadeMusicComplete");  
            }
            else
            {
                audioMusicRun.clip = listBgMusic[musicN];
                audioMusicRun.volume=0;
                audioMusicRun.Play();
                audioMusicRun.DOFade(0.35f, fadeDuratyion);
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
    public void PlayMusicBGFadeOut(int musicN,float fadeDuration) {
        if (Config.isMusic)
        {
            if (musicN==0)
            {
                audioMusic.volume = 0.35f;
                audioMusic.Play();
                audioMusic.DOFade(0f, fadeDuration);
            }
            else
            {
                audioMusicRun.volume = 0.35f;
                audioMusicRun.Play();
                audioMusicRun.DOFade(0f, fadeDuration);
            }
        }
    }

    public void StopMusicBG() {
        audioMusic.Stop();
        audioMusicRun.Stop();
        StopAudioLoop();
        
    }
    
    public void StopAudioLoop()
    {
        if (playAudioCoroutine != null)
        {
            StopCoroutine(playAudioCoroutine); // Останавливаем корутину
            playAudioCoroutine = null;
            audioMusicRun.Stop(); // Останавливаем текущий проигрываемый звук
        }
    }
    public void EnableMusic()
    {
        Config.SetMusic(true);
        audioMusic.volume=1;
        audioMusicRun.volume = 1;
    }
    
    public void DisableMusic()
    {
        Config.SetMusic(false);
        
        audioMusic.volume=0;
        audioMusicRun.volume = 0;

    }
    
}
