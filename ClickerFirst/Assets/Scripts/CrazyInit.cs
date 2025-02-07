using System;
using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using UnityEngine;

public class CrazyInit : MonoBehaviour
{
    public static event Action OnCrazyInitialized;
    // Start is called before the first frame update
    void Start()
    {
        CrazySDK.Init(OnCrazyInit);
    }

    private void OnCrazyInit()
    {
        OnCrazyInitialized();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
