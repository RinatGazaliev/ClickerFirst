using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegInPortal : MonoBehaviour
{
    
    public static event Action OnLegHidden;
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
}
