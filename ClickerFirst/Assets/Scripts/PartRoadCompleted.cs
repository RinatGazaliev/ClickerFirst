using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class PartRoadCompleted : MonoBehaviour
{
    public static event Action OnPartRoadCompletedActive;
    public static event Action OnPartRoadCompletedClosed;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnOkTut1;
    
    [SerializeField] private Button btnOkTut2;
    [SerializeField] private Button btnOkTut3;
    [SerializeField] public GameObject defaultView;
    [SerializeField] public GameObject Tut1;
    [SerializeField] public GameObject Tut2;
    [SerializeField] public GameObject Tut3;

    [SerializeField] private Image coinAnimate;
    [SerializeField] private Image kickAnimate;
    [SerializeField] private Image clothesAnimate;

    [SerializeField] private LeftButtZoneManager rewardZone;
   // public float arcHeightMultiplier = 10f; // Коэффициент для увеличения амплитуды дуги

    
    
    
    [SerializeField] private Image bgImg;
    // Start is called before the first frame update
    void Start()
    {
        btnContinue.onClick.AddListener(ContinuePressed);
      //  bgImg = GetComponent<Image>();
        Vector3 coordTut1FinalPosition = rewardZone.rewDoublePoints.transform.position;
        
        btnOkTut1.onClick.AddListener(() => MoveToTarget(3,coinAnimate, rewardZone.rewDoublePoints.transform.position, 0.5f , rewardZone.rewDoublePoints.gameObject, Tut1, btnOkTut1));
        btnOkTut2.onClick.AddListener(() => MoveToTarget(2, kickAnimate, rewardZone.rewMoveBoost.transform.position, 0.5f , rewardZone.rewMoveBoost.gameObject, Tut2, btnOkTut2));
        btnOkTut3.onClick.AddListener(() => MoveToTarget(1, clothesAnimate, rewardZone.rewGetEquip.transform.position, 0.5f , rewardZone.rewGetEquip.gameObject, Tut3, btnOkTut3));
       
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

    public void MoveToTarget(float arcHeightMultiplier, Image obj, Vector3 targetPosition, float duration , GameObject finalBtnReward, GameObject thisTut, Button thisButt)
    {
        OnPartRoadCompletedClosed();
        thisTut.transform.Find("vfxRotation").gameObject.SetActive(false);
        thisButt.gameObject.SetActive(false);
        bgImg.gameObject.SetActive(false);
        RectTransform rectTransform = obj.rectTransform;
        Vector3 startPos = rectTransform.position;
        Vector3 midPoint = (startPos + targetPosition) / 2 + Vector3.up * Mathf.Abs(targetPosition.y - startPos.y) * arcHeightMultiplier;
        
        // Создаем путь в виде дуги
        Vector3[] path = new Vector3[] { startPos, midPoint, targetPosition };
        
        // Анимация движения по траектории
        rectTransform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(Ease.OutQuad);
        
        // Анимация уменьшения масштаба до 0
        rectTransform.DOScale(Vector3.zero, duration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                rewardZone.AppearWithScale(finalBtnReward, 0.5f, thisTut.name);
                thisTut.SetActive(false);
                bgImg.gameObject.SetActive(true);
                gameObject.SetActive(false);
                });
    }
}
