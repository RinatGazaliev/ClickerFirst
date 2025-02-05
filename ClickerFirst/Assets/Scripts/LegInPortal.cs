using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegInPortal : MonoBehaviour
{
    
    public static event Action OnLegHidden;
    public static event Action OnKickedAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideLeg()
    {
        OnLegHidden();
        gameObject.SetActive(false);
    }
    public void CallKickEvent()
    {
        PlayPunchLeg();
        OnKickedAnim();
    }
    public void PlayPunchLeg()
    {
        SoundManager.instance.PlaySound_punchLeg();
    }
}
