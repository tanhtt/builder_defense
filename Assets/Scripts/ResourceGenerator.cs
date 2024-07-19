using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private float timer;
    private float timerMax;

    private void Awake()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        timerMax = buildingType.generatorData.timerMax;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer += timerMax;
            Debug.Log("Ding" + buildingType.generatorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(buildingType.generatorData.resourceType, 1);
        }
    }
}
