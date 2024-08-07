using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingParticleSpawner : Spawner
{
    private static BuildingParticleSpawner instance;
    public static BuildingParticleSpawner Instance => instance;

    private Transform pfBuildingDestroyedParticles;
    private Transform pfBuildingPlacedParticles;
    private Transform pfBuildingConstruction;

    protected void Awake()
    {
        if (BuildingParticleSpawner.instance != null) Debug.LogError("Only 1 BuildingParticleSpawner allow to exist");
        BuildingParticleSpawner.instance = this;
    }

    protected override void LoadAssets()
    {
        // Get pfBuildingDestroyedParticles
        if (pfBuildingDestroyedParticles == null) pfBuildingDestroyedParticles = GameAssets.Instance.pfBuildingDestroyedParticles;
        if (pfBuildingDestroyedParticles != null)
        {
            this.prefabs.Add(pfBuildingDestroyedParticles);
        }
        else
        {
            Debug.LogError("pfBuildingDestroyedParticles prefab not found in GameAssets");
        }

        // Get pfBuildingPlacedParticles
        if (pfBuildingPlacedParticles == null) pfBuildingPlacedParticles = GameAssets.Instance.pfBuildingPlacedParticles;
        if (pfBuildingPlacedParticles != null)
        {
            this.prefabs.Add(pfBuildingPlacedParticles);
        }
        else
        {
            Debug.LogError("pfBuildingPlacedParticles prefab not found in GameAssets");
        }

        // Get pfBuildingConstruction
        if (pfBuildingConstruction == null) pfBuildingConstruction = GameAssets.Instance.pfBuildingConstruction;
        if (pfBuildingConstruction != null)
        {
            this.prefabs.Add(pfBuildingConstruction);
        }
        else
        {
            Debug.LogError("pfBuildingConstruction prefab not found in GameAssets");
        }
    }
}
