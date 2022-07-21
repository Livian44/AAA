using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.Instance.OnGameEnd.AddListener(ShowPopup);
    }

    public void ShowPopup()
    {
        Debug.Log("I have ended the game");
        score = GameplayManager.Instance.PlayerController.CurrentScore;
       
        if(score < 100)
        {
            firstTierImage.color = Color.red;
        } else if(score < 300)
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
