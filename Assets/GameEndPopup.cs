using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameEndPopup : MonoBehaviour
{
    [SerializeField]
    private Image firstTierImage;

    [SerializeField]
    private Image secondTierImage;

    [SerializeField]
    private Image thirdTierImage;

    [SerializeField]
    private int score;

    public UnityEvent OnGameEnd;

    // Start is called before the first frame update
    void Start()
    {
        ShowPopup();
    }

    public void ShowPopup()
    {
        Debug.Log("I have ended the game");
        score = GameplayManager.Instance.PlayerController.CurrentScore;

        //Time.timeScale = 0;

        if (score <= 500)
        {
            firstTierImage.color = Color.red;
        } else if(score <= 1000)
        {
            firstTierImage.color = Color.red;
            secondTierImage.color = Color.red;
        }
        else
        {
            firstTierImage.color = Color.red;
            secondTierImage.color = Color.red;
            thirdTierImage.color = Color.red;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
