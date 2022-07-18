using System.Collections;
using System.Collections.Generic;
using BuildingSystem.Interfaces;
using DefaultNamespace;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] 
    private GameObject basicTower;
    [SerializeField] 
    private GameObject basicTower2;
    [SerializeField] 
    private GameObject projectileTower;

    private bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SelectTower(basicTower);
        }
        
        if (Input.GetKeyDown("2"))
        {
            SelectTower(basicTower2);
        }
        
        if (Input.GetKeyDown("3"))
        {
            SelectTower(projectileTower);
        }

        if (selected && Input.GetMouseButtonDown(0))
        {
            TowerPlacementController towerPlacementController = FindObjectOfType<TowerPlacementController>();
            Vector3Int position = towerPlacementController.GetClosestTilePosition(Input.mousePosition);
            bool isTileFree = towerPlacementController.IsTileAvaliable(position);
            if (isTileFree)
            {
                towerPlacementController.BuildOnSpot(Input.mousePosition);
                selected = false;
                towerPlacementController.ToggleTilemapVisible(false);
                Debug.Log("Building!");
            }
            else
            {
                Debug.Log("Not building");
            }
        }
    }

    private void SelectTower(GameObject targetTower)
    {
        TowerPlacementController towerPlacementController = FindObjectOfType<TowerPlacementController>();
        towerPlacementController.towerSelected = targetTower.GetComponent<ITower>();
        selected = true;
        towerPlacementController.ToggleTilemapVisible(true);
    }
}
