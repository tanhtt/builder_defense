using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectileSpawner : Spawner
{
    private static ArrowProjectileSpawner instance;
    public static ArrowProjectileSpawner Instance => instance;

    private Transform pfArrowProjectile;

    protected void Awake()
    {
        if (ArrowProjectileSpawner.instance != null) Debug.LogError("Only 1 ArrowProjectileSpawner allow to exist");
        ArrowProjectileSpawner.instance = this;
    }

    protected override void LoadAssets()
    {
        if (pfArrowProjectile == null) pfArrowProjectile = GameAssets.Instance.pfArrowProjectile;
        if (pfArrowProjectile != null)
        {
            this.prefabs.Add(pfArrowProjectile);
        }
        else
        {
            Debug.LogError("Arrow Projectile prefab not found in GameAssets");
        }
    }
}
