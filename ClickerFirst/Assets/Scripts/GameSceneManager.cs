using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
       // Config.isMusic = true;
        MusicManager.instance.PlayMusicBGFadeIn(false,2);
        
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
