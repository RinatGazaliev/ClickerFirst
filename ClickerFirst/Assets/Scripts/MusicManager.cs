using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    
    public static MusicManager instance;
    [SerializeField] AudioSource audioMusic;
    [SerializeField] AudioSource audioSoundEffect;
    private Coroutine playAudioCoroutine;

    public List<AudioClip> listBgMusic;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        audioMusic = GetComponent<AudioSource>();

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
        if (Config.isMusic)
        {
            audioMusic.clip = listBgMusic[musicN];
            audioMusic.volume=0;
            audioMusic.Play();
            audioMusic.DOFade(0.35f, fadeDuratyion);
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
    public void PlayMusicBGFadeOut(float fadeDuration) {
        if (Config.isMusic)
        {
            if (audioMusic.clip != null)
            {
                audioMusic.volume = 0.35f;
                audioMusic.Play();
                audioMusic.DOFade(0f, fadeDuration);
            }
        }
    }

    public void StopMusicBG() {
        audioMusic.Stop();
        audioSoundEffect.Stop();
        StopAudioLoop();
        
    }
    
    public void StopAudioLoop()
    {
        if (playAudioCoroutine != null)
        {
            StopCoroutine(playAudioCoroutine); // Останавливаем корутину
            playAudioCoroutine = null;
            audioSoundEffect.Stop(); // Останавливаем текущий проигрываемый звук
        }
    }
    
}
