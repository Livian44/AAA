using UnityEngine;

namespace BuildingSystem.Interfaces
{
    public interface ITower
    {
        TowerType TowerType { get; set; }
        GameObject GameObject { get;  }
    }
}