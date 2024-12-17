using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Header("RewardAutoClick")] 
    [SerializeField] private Button btnReward_Autoclick;
    [SerializeField] private float timerAutoclick;
    //TODO Добавить делегат на ревард клик
    
    [Header("Reward_x2Points")] 
    [SerializeField] private Button btnReward_DoublePoints;
    [SerializeField] private float timerDoublePoints;
    //TODO Добавить делегат на ревард клик
    
    [Header("Reward_MoveBoost")] 
    [SerializeField] private Button btnReward_MoveBoost;
    [SerializeField] private float timerMoveBoosts;
    //TODO Добавить делегат на ревард кл


}
