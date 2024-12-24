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

    [SerializeField] private float kickInterval = 5f;
    [SerializeField] private Slider timerSlider;// Таймер для события OnAutoClick
    [SerializeField] private Animator animContrCharacter;
    
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
        //animContrCharacter.speed=1.3f;
        
        animContrCharacter.SetTrigger("KickAnim");
        animContrCharacter.SetFloat("walkSpeed",1.3f);
        animContrCharacter.SetFloat("runSpeed",1.3f);
       // animContrCharacter.SetBool("isKicked", true);
        isMoveBoostRunning = true;
        Config.SetMoveBoostRewValue(1.5f);
        Debug.Log("Auto-click started");
        float elapsedTime = 0f;
        float timerKick = 0f;

        while (elapsedTime < moveBoostDuration)
        {
            elapsedTime += Time.deltaTime;
            timerKick += Time.deltaTime;

            // Обновляем значение слайдера
            if (timerSlider != null)
            {
                timerSlider.value = 1f - (elapsedTime / moveBoostDuration);
            }
            if (timerKick >= kickInterval)
            {
                timerKick = 0f; // Сброс таймера
                animContrCharacter.SetTrigger("KickAnim");
            }

            yield return null;
        }

        if (timerSlider != null)
        {
            timerSlider.value = 0f;
        }
        
        Config.SetMoveBoostRewValue(1f);
        isMoveBoostRunning = false;
        //animContrCharacter.SetBool("isKicked", false);
       // animContrCharacter.Play("1_Idle_1");
        //animContrCharacter.speed=1f;
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
