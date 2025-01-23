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
        MusicManager.instance.PlayMusicBGFadeIn(0,2);
        
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
        if (isRunning)
        {
            MusicManager.instance.PlayMusicBGFadeOut(0, 2);
            MusicManager.instance.PlayMusicBGFadeIn(1,6);
        }
        else
        {
            MusicManager.instance.PlayMusicBGFadeOut(1, 2);
            MusicManager.instance.PlayMusicBGFadeIn(0,6);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
