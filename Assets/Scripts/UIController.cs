using System.Collections;
using System.Collections.Generic;
using BuildingSystem.Interfaces;
using DefaultNamespace;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject test;

    private bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
        {
            TowerPlacementController towerPlacementController = FindObjectOfType<TowerPlacementController>();
            towerPlacementController.towerSelected = test.GetComponent<ITower>();
            selected = true;
        }

        if (selected && Input.GetMouseButtonDown(0))
        {
            TowerPlacementController towerPlacementController = FindObjectOfType<TowerPlacementController>();
            Vector3Int position = towerPlacementController.GetClosestTilePosition(Input.mousePosition);
            Debug.Log(position);
            bool isTileFree = towerPlacementController.IsTileAvaliable(position);
            if (isTileFree)
            {
                towerPlacementController.BuildOnSpot(Input.mousePosition);
                Debug.Log("Building!");
            }
            else
            {
                Debug.Log("Not building");
            }
        }
    }
}
