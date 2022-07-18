using BuildingSystem.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] 
        private Image icon;
        [SerializeField] 
        private TMP_Text price;


        private ITower assignedTower;
        private TowerType assignedTowerType;
        private PlayerController playerController;
        private Button button;
        private UIController uiController;

        private void Start()
        {
            playerController = GameplayManager.Instance.PlayerController;
            playerController.OnPlayerMoneyChange.AddListener(UpdateButtonState);
            button = GetComponent<Button>();
            button.onClick.AddListener(SelectTower);
        }
        
        public void Initialize(ITower tower, UIController uiController)
        {
            assignedTower = tower;
            assignedTowerType = assignedTower.TowerType;
            icon.sprite = assignedTowerType.Sprite;
            price.text = "$"+assignedTowerType.Price;
            this.uiController = uiController;
        }

        private void UpdateButtonState(int money)
        {
            button.interactable = playerController.HasEnoughMoney(assignedTowerType.Price);
        }

        private void SelectTower()
        {
            uiController.SelectTower(assignedTower);
        }
    }
}