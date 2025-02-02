using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using DepthOfField = UnityEngine.Rendering.Universal.DepthOfField;

public class FeelEffectsManager : MonoBehaviour
{
    [Header("FeelKick")]
    [SerializeField] private MMF_Player kickEffect;
    [SerializeField] private MMF_Player kickEffectTEST;
    
    [Header("FeelRunBlur")]
    [SerializeField] private Volume blurEffect;
    [SerializeField] private VolumeProfile blurEffectProfiler;
    [SerializeField] private float minFocalLength = 70f; // Минимальное значение
    [SerializeField] private float maxFocalLength = 120f; // Максимальное значение
    private DepthOfField DoF;

    private float currentFocalLength;
    // Start is called before the first frame update
    private void OnEnable()
    {
        LegInPortal.OnKickedAnim += CallKickFeel;
        ForkBar.OnForkBarIsRunning += CallKickFeelTEST;
    }

    private void OnDisable()
    {
        LegInPortal.OnKickedAnim -= CallKickFeel;
        ForkBar.OnForkBarIsRunning -= CallKickFeelTEST;
    }

    void Start()
    {
        
        if (blurEffectProfiler.TryGet<DepthOfField>(out var dof))
        {
            DoF = dof;
            currentFocalLength = minFocalLength;
            DoF.focalLength.value = currentFocalLength;
        }
        else
        {
            Debug.LogError("DepthOfField не найден в VolumeProfile!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CallKickFeel()
    {
        kickEffect.PlayFeedbacks();
    }
    
    private void CallKickFeelTEST(bool _isRunning)
    {
        if (_isRunning)
        {
            kickEffectTEST.PlayFeedbacks(); 
        }
        else
        {
            //kickEffectTEST.StopAllCoroutines();
            kickEffectTEST.StopFeedbacks();
        }
        
    }
    
    private void CallBlur(bool _isRunning)
    {
        if (_isRunning)
        {
            Debug.Log("RunningFeel");
           // dof.focalLength.value = 200f;
            AnimateFocalLength(minFocalLength, maxFocalLength, 1.5f);
        }
        else
        {
            AnimateFocalLength(maxFocalLength, minFocalLength, 1.5f);
        }
       
    }
    
    private void AnimateFocalLength(float startValue, float endValue, float duration)
    {
        if (DoF == null) return;
        Debug.Log("currentFocalLength"+currentFocalLength);
        currentFocalLength = startValue;
        DoF.focalLength.value = currentFocalLength;

        DOTween.To(() => currentFocalLength, x =>
            {
                currentFocalLength = x;
                
                DoF.focalLength.value = currentFocalLength;
            }, endValue, duration)
            .SetEase(Ease.InOutSine);
    }
}
