using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance {  get; private set; }

    public event EventHandler OnWaveNumberChanger;

    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave
    }

    [SerializeField] private List<Transform> spawnPosTransform;
    [SerializeField] private Transform nextWaveSpawnPosTransform;

    private State state;
    private int waveNumber;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = State.WaitingToSpawnNextWave;
        spawnPos = spawnPosTransform[UnityEngine.Random.Range(0, spawnPosTransform.Count)].position;
        nextWaveSpawnPosTransform.position = spawnPos;
        nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0)
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0, .2f);
                        Enemy.Create(spawnPos + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0, 10));
                        remainingEnemySpawnAmount--;

                        if(remainingEnemySpawnAmount <= 0)
                        {
                            state = State.WaitingToSpawnNextWave;
                            spawnPos = spawnPosTransform[UnityEngine.Random.Range(0, spawnPosTransform.Count)].position;
                            nextWaveSpawnPosTransform.position = spawnPos;
                            nextWaveSpawnTimer = 10f;
                        }
                    }
                }
                break;
        }
    }

    private void SpawnWave()
    {
        remainingEnemySpawnAmount = 5 + 3 * waveNumber;
        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanger?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPos()
    {
        return spawnPos;
    }
}
