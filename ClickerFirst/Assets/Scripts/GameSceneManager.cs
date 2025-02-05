using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GameSceneManager : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
       // Config.isMusic = true;
        YG2.GameReadyAPI();
        YG2.GameplayStart();
        MusicManager.instance.PlayMusicBGFadeIn(false,2);
       //Config.SetTutN(0);
    }

    private void OnEnable()
    {
        ForkBar.OnForkBarIsRunning += ChangeMusicRunning;
    }
    private void OnDisable()
    {
        ForkBar.OnForkBarIsRunning -= ChangeMusicRunning;
    }

    private void ChangeMusicRunning(bool isRunning)
    {
        MusicManager.instance.SwapMusic(isRunning, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
