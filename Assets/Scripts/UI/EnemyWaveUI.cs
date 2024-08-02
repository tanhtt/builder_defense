using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    private RectTransform enemyWaveSpawnPosIndicator;
    private RectTransform enemyClosetPosIndicator;

    private Camera mainCamera;

    private void Awake()
    {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnPosIndicator = transform.Find("enemyWaveSpawnPosIndicator").GetComponent<RectTransform>();
        enemyClosetPosIndicator = transform.Find("enemyClosetPosIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanger += EnemyWaveManager_OnWaveNumberChanger;
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void EnemyWaveManager_OnWaveNumberChanger(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave "+ enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        this.HandleNextWaveMessage();
        this.HandleEnemyWaveSpawnPositionIndicator();
        this.HandleEnemyClosetPositionIndicator();
    }

    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer < 0)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void HandleEnemyWaveSpawnPositionIndicator()
    {
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPos() - mainCamera.transform.position).normalized;
        enemyWaveSpawnPosIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        enemyWaveSpawnPosIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPos = Vector3.Distance(enemyWaveManager.GetSpawnPos(), mainCamera.transform.position);
        enemyWaveSpawnPosIndicator.gameObject.SetActive(distanceToNextSpawnPos > mainCamera.orthographicSize * 1.5f);
    }

    private void HandleEnemyClosetPositionIndicator()
    {
        float targetMaxRadius = 9999f;
        Collider2D[] col2DArr = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        Enemy targetEnemy = null;
        foreach (Collider2D col in col2DArr)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        //Closer
                        targetEnemy = enemy;
                    }
                }
            }
        }

        if (targetEnemy != null)
        {
            Vector3 dirToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;
            enemyClosetPosIndicator.anchoredPosition = dirToClosestEnemy * 250f;
            enemyClosetPosIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);
            enemyClosetPosIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
        }
        else
        {
            enemyClosetPosIndicator.gameObject.SetActive(false);
        }
    }

    private void SetMessageText(string message)
    {
        this.waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        this.waveNumberText.SetText(text);
    }
}
