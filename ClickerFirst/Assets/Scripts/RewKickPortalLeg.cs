using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewKickPortalLeg : MonoBehaviour
{
    [SerializeField] private GameObject kickLeg;
    [SerializeField] private GameObject portal;

    private Animator animatorKickLeg;
    private Animator animatorPortal;
    
    // Start is called before the first frame update
    void Start()
    {
        animatorKickLeg = kickLeg.GetComponent<Animator>();
        animatorPortal = portal.GetComponent<Animator>();
        InitViews();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitViews()
    {
        kickLeg.SetActive(false);
        portal.SetActive(false);
    }
    
    private void OnEnable()
    {
        RewMoveBoost.OnRewardStarted += RewardWatchedEvent;
        RewMoveBoost.OnRewardMoveBoostTimeFinish += EndRewTime;
        RewMoveBoost.OnKickCalled += KickCalled;
    }

    private void OnDisable()
    {
        RewMoveBoost.OnRewardStarted -= RewardWatchedEvent;
        RewMoveBoost.OnRewardMoveBoostTimeFinish -= EndRewTime;
        RewMoveBoost.OnKickCalled -= KickCalled;

    }

    private void RewardWatchedEvent()
    {
        kickLeg.SetActive(true);
        //animatorKickLeg.SetTrigger("Kick");
        
        portal.SetActive(true);
        animatorPortal.Play("1_PortalAppear");
        animatorKickLeg.Play("1_KickAppear");
       
        
    }

    private void KickCalled()
    {
        animatorKickLeg.SetTrigger("Kick");
    }
    
    private void EndRewTime()
    {
        animatorKickLeg.SetTrigger("KickDisappear");
       // animatorPortal.SetTrigger("PortalDisappear");
    }

}
