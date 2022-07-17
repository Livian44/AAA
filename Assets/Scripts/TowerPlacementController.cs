using BuildingSystem.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class TowerPlacementController : MonoBehaviour
    {
        [SerializeField] 
        private Tilemap towerTilemap;
        [SerializeField] 
        private Grid towerGrid;
        [SerializeField] 
        private Camera camera;

        public ITower towerSelected;

        private TilemapRenderer tilemapRenderer;
        
        private void Start()
        {
            tilemapRenderer = towerTilemap.GetComponent<TilemapRenderer>();
        }
        
        public void ToggleTilemapVisible(bool isVisible)
        {
            tilemapRenderer.enabled = isVisible;
        }

        public void SetSelectedTower(ITower tower)
        {
            towerSelected = tower;
        }
        
        public void BuildOnSpot(Vector3 screenMousePosition)
        {
            Vector3 mousePosition = camera.ScreenToWorldPoint(screenMousePosition);
            Vector3 mapPoint = GetMapPoint(mousePosition);
            Vector3 worldPoint = GetGridPosition(mousePosition);
            worldPoint += new Vector3(0.5f,0.5f,towerTilemap.transform.position.z+0.1f);
            Instantiate(towerSelected.GameObject, worldPoint, Quaternion.identity);
            MarkAsUnavaliable(GetGridPosition(mapPoint));
        }

        public Vector3Int GetClosestTilePosition(Vector3 screenMousePosition)
        {
            Vector3 mousePosition = camera.ScreenToWorldPoint(screenMousePosition);
            Vector3 worldPoint = GetWorldPoint(mousePosition);
            return towerGrid.WorldToCell(worldPoint);
        }

        public bool IsTileAvaliable(Vector3Int gridPosition)
        {
            return towerTilemap.GetTile(gridPosition) != null;
        }

        public Vector3Int GetGridPosition(Vector3 mapPoint)
        {
            return towerGrid.WorldToCell(mapPoint);
        }

        public static Vector3 GetWorldPoint(Vector3 inputDataPointerWorldPosition)
        {
            Vector3 worldPoint = inputDataPointerWorldPosition;
            worldPoint.z = 0;

            return worldPoint;
        }

        public static Vector3 GetMapPoint(Vector3 inputDataPointerWorldPosition)
        {
            Vector3 mapPoint = inputDataPointerWorldPosition;
            mapPoint.z = 0;

            return mapPoint;
        }
        
        public void MarkAsUnavaliable(Vector3Int position)
        {
            towerTilemap.SetTile(position,null);
        }
    }
}