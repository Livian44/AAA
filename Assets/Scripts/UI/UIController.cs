using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.Interfaces;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] 
    private GameObject basicTower;
    [SerializeField] 
    private GameObject basicTower2;
    [SerializeField] 
    private GameObject projectileTower;
    [SerializeField] 
    private TMP_Text moneyText;
    [SerializeField] 
    private TMP_Text scoreText;
    [SerializeField] 
    private GameObject towerButtonPrefab;
    [SerializeField] 
    private GameObject towerButtonParent;
    [SerializeField]
    private TowerPlacementController towerPlacementController;
    
    private bool selected = false;

    private List<ITower> currentAvaliableTowers = new List<ITower>();

    private List<GameObject> towerButtons = new List<GameObject>();
    PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        
        playerController = GameplayManager.Instance.PlayerController;
        GetAvaliableTowers();
        DestroyTowerButtons();
        CreateTowerButtons();
        AssignEvents();
        UpdateScoreText(playerController.CurrentScore);
        UpdateMoneyText(playerController.CurrentMoney);
    }

    private void DestroyTowerButtons()
    {
        foreach (Transform towerButton in towerButtonParent.transform)
        {
            if (towerButton != towerButtonParent.transform)
            {
                Destroy(towerButton.gameObject);
            }
        }
    }

    private void AssignEvents()
    {
        playerController.OnPlayerMoneyChange.AddListener(UpdateMoneyText);
        playerController.OnPlayerScoreChange.AddListener(UpdateScoreText);
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }

    private void UpdateMoneyText(int money)
    {
        moneyText.text = "$" + money;
    }

    private void CreateTowerButtons()
    {
        foreach (ITower towerType in currentAvaliableTowers)
        {
            GameObject towerButton = Instantiate(towerButtonPrefab, towerButtonParent.transform);
            TowerButton towerPlacementButton = towerButton.GetComponent<TowerButton>();
            towerPlacementButton.Initialize(towerType,this);
            towerButtons.Add(towerButton);
        }
    }

    private void GetAvaliableTowers()
    {
        List<GameObject> towers = GameplayManager.Instance.Level.AvaliableTowers.Towers;
        currentAvaliableTowers = towers.Select(tower => tower.GetComponent<ITower>()).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected && Input.GetMouseButtonDown(0))
        {
            Vector3Int position = towerPlacementController.GetClosestTilePosition(Input.mousePosition);
            bool isTileFree = towerPlacementController.IsTileAvaliable(position);
            if (isTileFree)
            {
                towerPlacementController.BuildOnSpot(Input.mousePosition);
                selected = false;
                towerPlacementController.ToggleTilemapVisible(false);
                Debug.Log("Building!");
                playerController.DecreasePlayerMoney(towerPlacementController.towerSelected.TowerType.Price);
            }
            else
            {
                Debug.Log("Not building");
            }
        }
    }

    public void SelectTower(ITower targetTower)
    {
        towerPlacementController.towerSelected = targetTower;
        selected = true;
        towerPlacementController.ToggleTilemapVisible(true);
    }
}
