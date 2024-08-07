using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class ArrowProjectile : MonoBehaviour
{
    public static ArrowProjectile Create(Vector3 position, Enemy targetEnemy)
    {
        //Transform arrowProjectileTransform = Instantiate(GameAssets.Instance.pfArrowProjectile, position, Quaternion.identity);
        Transform arrowProjectileTransform = ArrowProjectileSpawner.Instance.Spawn(GameAssets.Instance.pfArrowProjectile.name, position, Quaternion.identity);
        arrowProjectileTransform.gameObject.SetActive(true);

        ArrowProjectile arrowProjectile = arrowProjectileTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(targetEnemy);
        return arrowProjectile;
    }

    private Enemy targetEnemy;
    private Vector3 lastMoveDir;
    [SerializeField] private float timeToDie = 3f;
    private float initialTimeToDie = 3f;

    private void OnEnable()
    {
        timeToDie = initialTimeToDie; // Reset timeToDie when enabled
    }

    private void Update()
    {
        MovingArrowProjectile();
        CountDownToDestroy();
    }

    private void MovingArrowProjectile()
    {
        Vector3 moveDir;
        if (targetEnemy.gameObject.activeSelf)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        float moveSpeed = 10f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void CountDownToDestroy()
    {
        timeToDie -= Time.deltaTime;
        if (timeToDie < 0f)
        {
            if (!this.transform.gameObject.activeSelf) return;
            ArrowProjectileSpawner.Instance.Despawn(this.transform);
            //Destroy(this.gameObject);
        }
    }

    private void SetTarget(Enemy target)
    {
        this.targetEnemy = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null && enemy.gameObject.activeSelf)
        {
            // Hit an enemy
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().TakeDamage(damageAmount);
            //Destroy(this.gameObject);
            ArrowProjectileSpawner.Instance.Despawn(this.transform);
        }
    }
}
