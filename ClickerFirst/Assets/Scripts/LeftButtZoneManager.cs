using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LeftButtZoneManager : MonoBehaviour
{
    [SerializeField] public RewAutoClicker rewAutoClicker;
    [SerializeField] public RewDoublePoints rewDoublePoints;
    [SerializeField] public RewMoveBoost rewMoveBoost;
    [SerializeField] public RewGetEquip rewGetEquip;
    [SerializeField] public  GameObject equipShop;

    private int tutN;
    // Start is called before the first frame update
    
    public static event Action<string> OnTutAnimFinished = delegate (string _tutName) { };
    void Start()
    {
        tutN = Config.GetTutN();
        InitViews();
        StartCoroutine(StartAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitViews()
    {
        rewAutoClicker.gameObject.SetActive(false);
        rewDoublePoints.gameObject.SetActive(false);
        rewMoveBoost.gameObject.SetActive(false);
        rewGetEquip.gameObject.SetActive(false);
        equipShop.SetActive(false);
    }

    private IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(0.1f);
        rewAutoClicker.gameObject.SetActive(true);
        rewAutoClicker.GetComponent<UIAnimation>().CallAnimationFunct();

        if (tutN>=1)
        {
            yield return new WaitForSeconds(0.1f);
            rewDoublePoints.gameObject.SetActive(true);
            rewDoublePoints.GetComponent<UIAnimation>().CallAnimationFunct();
        }


        if (tutN >= 2)
        {
            yield return new WaitForSeconds(0.1f);
            rewMoveBoost.GetComponent<UIAnimation>().CallAnimationFunct();
            rewMoveBoost.gameObject.SetActive(true);
        }

        if (tutN >= 3)
        {
            yield return new WaitForSeconds(0.1f);
            equipShop.GetComponent<UIAnimation>().CallAnimationFunct();
            equipShop.SetActive(true);

            yield return new WaitForSeconds(0.1f);
            rewGetEquip.GetComponent<UIAnimation>().CallAnimationFunct();
            rewGetEquip.gameObject.SetActive(true);
        }

    }
    
    public void AppearWithScale(GameObject obj, float duration, string tutName)
    {
        Vector3 finalScale = obj.transform.localScale;
        obj.transform.localScale = Vector3.zero;
        obj.SetActive(true);
        obj.transform.DOScale(finalScale, duration).SetEase(Ease.OutBack)
            .OnComplete(() => OnTutAnimFinished(tutName));
    }
}
