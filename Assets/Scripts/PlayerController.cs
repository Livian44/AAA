using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent<int> OnPlayerScoreChange;

    public UnityEvent<int> OnPlayerMoneyChange;

    [field:SerializeField]
    public int CurrentScore { get; set; }= 0;
    
    [field:SerializeField]
    public int CurrentMoney  { get; set; } = 10;

    public void IncreasePlayerScore(int score)
    {
        CurrentScore += score;
        OnPlayerScoreChange.Invoke(CurrentScore);
    }
    
    public void IncreasePlayerMoney(int money)
    {
        CurrentMoney += money;
        OnPlayerMoneyChange.Invoke(CurrentMoney);
    }
    
    public bool HasEnoughMoney(int moneyNeeded)
    {
        return CurrentMoney >= moneyNeeded;
    }
    public void DecreasePlayerMoney(int money)
    {
        CurrentMoney -= money;
        OnPlayerMoneyChange.Invoke(CurrentMoney);
    }
}
