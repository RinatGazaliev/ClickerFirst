using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewMoveBoost : MonoBehaviour
{
     private bool isMoveBoostRunning = false;
    private float moveBoostDuration = 15f; 
    //private float autoClickInterval = 0.5f; // Интервал между событиями OnAutoClick
    //private float autoClickTimer = 0f;
    private Button btnSelf;
    [SerializeField] private Slider timerSlider;// Таймер для события OnAutoClick
    
    public static event Action OnRewardMoveBoostTimeFinish;
    
    //public static event Action OnAutoClickerClick;
    // Start is called before the first frame update
    void Start()
    {
        if (timerSlider != null)
        {
            timerSlider.minValue = 0f;
            timerSlider.maxValue = 1f;
            timerSlider.value = 1f;
        }
        btnSelf=GetComponent<Button>();
        btnSelf.onClick.AddListener(OnPointerClick);
        InitViews();
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    
    private IEnumerator StartAutoClickTimer()
    {
        isMoveBoostRunning = true;
        Config.SetMoveBoostRewValue(1.5f);
        Debug.Log("Auto-click started");
        float elapsedTime = 0f;

        while (elapsedTime < moveBoostDuration)
        {
            elapsedTime += Time.deltaTime;

            // Обновляем значение слайдера
            if (timerSlider != null)
            {
                timerSlider.value = 1f - (elapsedTime / moveBoostDuration);
            }

            yield return null;
        }

        if (timerSlider != null)
        {
            timerSlider.value = 0f;
        }
        
        Config.SetMoveBoostRewValue(1f);
        isMoveBoostRunning = false;
        OnRewardMoveBoostTimeFinish();
        
        Debug.Log("Auto-click ended");
    }
    
    private void OnPointerClick ()
    {
        timerSlider.gameObject.SetActive(true);
        btnSelf.interactable = false;
        
        if (!isMoveBoostRunning) // Если таймер ещё не запущен
        {
            StartCoroutine(StartAutoClickTimer());
        }
    }

    public void InitViews()
    {
        timerSlider.gameObject.SetActive(false);
        btnSelf.interactable = true;
    }
}
