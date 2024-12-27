using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButtZoneManager : MonoBehaviour
{
    [SerializeField] private RewAutoClicker rewAutoClicker;
    [SerializeField] private RewDoublePoints rewDoublePoints;
    [SerializeField] private RewMoveBoost rewMoveBoost;
    [SerializeField] private RewGetEquip rewGetEquip;
    [SerializeField] private  GameObject equipShop;
    // Start is called before the first frame update
    void Start()
    {
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
        
        
        yield return new WaitForSeconds(0.1f);
        rewDoublePoints.gameObject.SetActive(true);
        rewDoublePoints.GetComponent<UIAnimation>().CallAnimationFunct();
        
        
        yield return new WaitForSeconds(0.1f);
        rewMoveBoost.GetComponent<UIAnimation>().CallAnimationFunct();
        rewMoveBoost.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
        equipShop.GetComponent<UIAnimation>().CallAnimationFunct();
        equipShop.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
        rewGetEquip.GetComponent<UIAnimation>().CallAnimationFunct();
        rewGetEquip.gameObject.SetActive(true);
        
    }
}
