using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public static ArrowProjectile Create(Vector3 position, Enemy targetEnemy)
    {
        Transform arrowProjectileTransform = Instantiate(GameAssets.Instance.pfArrowProjectile, position, Quaternion.identity);

        ArrowProjectile arrowProjectile = arrowProjectileTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(targetEnemy);
        return arrowProjectile;
    }

    private Enemy targetEnemy;
    private Vector3 lastMoveDir;
    private float timeToDie = 3f;

    private void Update()
    {
        Vector3 moveDir;
        if(targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        transform.eulerAngles = new Vector3(0,0, UtilsClass.GetAngleFromVector(moveDir));

        float moveSpeed = 10f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        timeToDie -= Time.deltaTime;
        if(timeToDie < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(Enemy target)
    {
        this.targetEnemy = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            // Hit an enemy
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
