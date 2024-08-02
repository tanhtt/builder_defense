using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] col2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;
        foreach (Collider2D col in col2DArray)
        {
            ResourceNode resourceNode = col.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }

    private ResourceGeneratorData generatorData;
    private float timer;
    private float timerMax;

    private void Awake()
    {
        generatorData = GetComponent<BuildingTypeHolder>().buildingType.generatorData;
        timerMax = generatorData.timerMax;
    }

    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(generatorData, transform.position);

        if (nearbyResourceAmount == 0)
        {
            // NO resource node nearby
            enabled = false;
        }
        else
        {
            timerMax = (generatorData.timerMax / 2f) + generatorData.timerMax * (1 - (float)nearbyResourceAmount / generatorData.maxResourceAmount);
        }

        //Debug.Log("nearbyResourceAmount: " + nearbyResourceAmount + "; timer max: " + timerMax);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer += timerMax;
            //Debug.Log("Ding: " + generatorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(generatorData.resourceType, 1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return this.generatorData;
    }

    public float GetTimerNormalize()
    {
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / timerMax;
    }
}
