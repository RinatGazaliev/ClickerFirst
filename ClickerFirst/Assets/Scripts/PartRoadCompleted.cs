using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartRoadCompleted : MonoBehaviour
{
    public static event Action OnPartRoadCompletedActive;
    public static event Action OnPartRoadCompletedClosed;
    [SerializeField] private Button btnContinue;
    
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
        Time.timeScale = 1f;
        
        Debug.Log("AddInterHere");
        gameObject.SetActive(false);
        // OnPartRoadCompletedActive();
    }
    
}
