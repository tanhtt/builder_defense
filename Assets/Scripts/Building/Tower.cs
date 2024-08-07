using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Enemy targetEnemy;
    private float lookForTargetTimer;
    [SerializeField] private float lookForTargetTimerMax = .2f;
    private float shootTimer;
    [SerializeField] private float shootTimerMax;
    private Vector3 projectileSpawnPos;

    private void Awake()
    {
        projectileSpawnPos = transform.Find("projectileSpawnPos").position;
    }

    private void Update()
    {
        this.HandleTargeting();
        this.HandleShooting();
    }


    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            this.LookForTargets();
        }
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer < 0f)
        {
            shootTimer += shootTimerMax;

            if (targetEnemy == null) return;
            if (!targetEnemy.gameObject.activeSelf) return;
            ArrowProjectile.Create(projectileSpawnPos, targetEnemy);
        }
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 30f;
        Collider2D[] col2DArr = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D col in col2DArr)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null && enemy.gameObject.activeSelf)
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
    }
}
