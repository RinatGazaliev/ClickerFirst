using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSoundButt : MonoBehaviour
{
    [SerializeField] private Sprite spriteON;
    [SerializeField] private Sprite spriteOFF;
    [SerializeField] private Image iconSound;
    private Button btnSwitchSound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        btnSwitchSound = GetComponent<Button>();
        btnSwitchSound.onClick.AddListener(OnButtTouch);
        //InitView();
        Config.SetSound(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Config.OnChangeIsSound += OnChangeIsSound;
    }
    
    private void OnDisable()
    {
        Config.OnChangeIsSound -= OnChangeIsSound;
    }

    private void OnChangeIsSound(bool _isSound)
    {
        if (_isSound)
        {
            iconSound.sprite = spriteON;
            MusicManager.instance.EnableMusic();
            SoundManager.instance.EnableSound();
        }
        else
        {
            iconSound.sprite = spriteOFF;
           
           // MusicManager.instance.StopMusicBG();
            MusicManager.instance.DisableMusic();
            SoundManager.instance.DisableSound();
        }
    }

    private void InitView()
    {
        OnChangeIsSound(Config.GetSound());
    }

    private void OnButtTouch()
    {
        Config.SetSound(!Config.isSound);
    }
}
