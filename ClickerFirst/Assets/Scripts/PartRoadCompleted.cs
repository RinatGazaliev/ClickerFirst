using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PartRoadCompleted : MonoBehaviour
{
    public static event Action OnPartRoadCompletedActive;
    public static event Action OnPartRoadCompletedClosed;
    [SerializeField] private Button btnContinue;
    [SerializeField] public GameObject defaultView;
    [SerializeField] public GameObject Tut1;
    [SerializeField] public GameObject Tut2;
    [SerializeField] public GameObject Tut3;
    
    // Start is called before the first frame update
    void Start()
    {
        btnContinue.onClick.AddListener(ContinuePressed); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPartRoadWdg()
    {
        Time.timeScale = 0f; 
       // OnPartRoadCompletedActive();
    }
    private void ContinuePressed()
    {
        Debug.Log("AddInterHere");
        YG2.InterstitialAdvShow();
        OnPartRoadCompletedClosed();
        gameObject.SetActive(false);

        // OnPartRoadCompletedActive();
    }
    
    
}
