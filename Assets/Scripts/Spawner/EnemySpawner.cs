using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance => instance;

    private Transform pfEnemy;

    protected void Awake()
    {
        if (EnemySpawner.instance != null) Debug.LogError("Only 1 EnemySpawner allow to exist");
        EnemySpawner.instance = this;
    }

    protected override void LoadAssets()
    {
        if (pfEnemy == null) pfEnemy = GameAssets.Instance.pfEnemy;
        if (pfEnemy != null)
        {
            this.prefabs.Add(pfEnemy);
        }
        else
        {
            Debug.LogError("Enemy prefab not found in GameAssets");
        }
    }
}
