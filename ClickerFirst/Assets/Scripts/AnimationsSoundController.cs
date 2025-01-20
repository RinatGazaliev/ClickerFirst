using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsSoundController : MonoBehaviour
{
    [Header("KickAppear")] 
    [SerializeField] private Animator kickAppear;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PlayKickAppearSound(string soundName)
    {
     SoundManager.instance.PlaySoundByName(soundName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
