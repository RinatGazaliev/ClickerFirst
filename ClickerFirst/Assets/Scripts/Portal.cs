using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    
    private Animator animatorPortal;
    // Start is called before the first frame update
    void Start()
    {
        animatorPortal = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void HidePortal()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        LegInPortal.OnLegHidden += DisappearPortal;
    }

    private void OnDisable()
    {
        LegInPortal.OnLegHidden -= DisappearPortal;
    }

    private void DisappearPortal()
    {
        animatorPortal.SetTrigger("PortalDisappear");
    }
    
    public void PlayPortalAppear()
    {
        SoundManager.instance.PlaySound_portalAppear();
    }
    
}
