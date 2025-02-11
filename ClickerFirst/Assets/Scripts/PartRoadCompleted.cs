using System;
using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using DG.Tweening;
using Gley.Localization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    [SerializeField] public GameObject EquipShopIcon;

    [SerializeField] private Image coinAnimate;
    [SerializeField] private Image kickAnimate;
    [SerializeField] private Image clothesAnimate;

    [SerializeField] private LeftButtZoneManager rewardZone;
   // public float arcHeightMultiplier = 10f; // Коэффициент для увеличения амплитуды дуги

    
    
    
    [SerializeField] private Image bgImg;

    [SerializeField] private CanvasGroup canvasGroup;
    
    [Header("JokeZone")]
    [SerializeField] private Text txtJoke;
    [SerializeField] private List<string> nameJokeLocalization;
    
    // Start is called before the first frame update
    void Start()
    {
       
        canvasGroup = GetComponent<CanvasGroup>();
        btnContinue.onClick.AddListener(ContinuePressed);
      //  bgImg = GetComponent<Image>();
        Vector3 coordTut1FinalPosition = rewardZone.rewDoublePoints.transform.position;

        btnOkTut1.onClick.AddListener(() =>
        {
            MoveToTarget(3, coinAnimate, rewardZone.rewDoublePoints.transform.position, 0.5f,
                rewardZone.rewDoublePoints.gameObject, Tut1, btnOkTut1);
            CrazySDK.Game.GameplayStart();
        });
        btnOkTut2.onClick.AddListener(() =>
        {
            MoveToTarget(2, kickAnimate, rewardZone.rewMoveBoost.transform.position, 0.5f,
                rewardZone.rewMoveBoost.gameObject, Tut2, btnOkTut2);
            CrazySDK.Game.GameplayStart();
        });
        btnOkTut3.onClick.AddListener(() => MoveToTarget(1, clothesAnimate, EquipShopIcon.transform.position, 0.5f , EquipShopIcon, Tut3, btnOkTut3));
        SetJokeText();
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
        SoundManager.instance.PlaySound_ButtClick();
        CrazySDK.Game.GameplayStart();
        Time.timeScale = 1f;
        Debug.Log("AddInterHere");
        //YG2.InterstitialAdvShow();
        //CrazySDK.Game.GameplayStop();
        CrazySDK.Ad.RequestAd(CrazyAdType.Midgame,OnInterStart,null, OnInterFinished);
        OnPartRoadCompletedClosed();
        SetJokeText();
        gameObject.SetActive(false);

        // OnPartRoadCompletedActive();
    }
    private void OnInterStart()
    {
        CrazySDK.Game.GameplayStop();
    }
    private void OnInterFinished()
    {
        CrazySDK.Game.GameplayStart();
    }

    public void MoveToTarget(float arcHeightMultiplier, Image obj, Vector3 targetPosition, float duration , GameObject finalBtnReward, GameObject thisTut, Button thisButt)
    {
        
        OnPartRoadCompletedClosed();
        if (thisTut==Tut3)
        {
            rewardZone.CallItemPopupTut();
        }
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
                //ShowWgtManager.instance.ShowNewItemPopUp();
                rewardZone.AppearWithScale(finalBtnReward, 0.5f, thisTut);
                //thisTut.SetActive(false);
                bgImg.gameObject.SetActive(true);
                gameObject.SetActive(false);
                });
    }

    private void SetJokeText()
    {
        int findJoke = Random.Range(0, nameJokeLocalization.Count);
        txtJoke.text = API.GetText(nameJokeLocalization[findJoke]);
    }

    public void AnimateAppearance(GameObject obj, float arcHeight, Vector3 offset, float animationTime)
    {
        //gameObject.SetActive(true);
        SoundManager.instance.PlaySound_FlagWgt();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1f, 0.3f).
            SetUpdate(true)
            .OnComplete(() =>     {
                Debug.Log($"animStarted for {obj?.name}");

                if (obj == null)
                {
                    Debug.Log("animQuitted - obj is null");
                    return;
                }
                // Сохраняем финальную позицию
                Vector3 finalPosition = obj.transform.position;

                // Выбираем стартовую точку (ниже и левее финальной)
                Vector3 startPosition = finalPosition + offset;
                startPosition.y -= arcHeight;

                // Скрываем объект и уменьшаем масштаб
                obj.transform.position = startPosition;
                obj.transform.localScale = Vector3.zero;
                obj.SetActive(true);
        
        
                obj.SetActive(true);
                DOTween.Init();
                obj.transform.DOKill(false);

                Debug.Log($"Before scaling: {obj.name}, scale: {obj.transform.localScale}");

                // Двигаем объект по дуге (если нужно)
                obj.transform.DOJump(finalPosition, arcHeight, 1, animationTime)
                    .SetEase(Ease.OutQuad)
                    .SetUpdate(true) // ✅ Игнорирует Time.timeScale
                    .OnComplete(() => Debug.Log($"DOJump finished for {obj.name}"));

                // Увеличиваем масштаб
                obj.transform.DOScale(Vector3.one, animationTime * 0.8f)
                    .SetEase(Ease.OutBack)
                    .SetUpdate(true) // ✅ Игнорирует Time.timeScale
                    .OnStart(() => Debug.Log($"DOScale started for {obj.name}"))
                    .OnComplete(() => Debug.Log($"DOScale finished for {obj.name}, final scale: {obj.transform.localScale}"));
            });
    }


}
