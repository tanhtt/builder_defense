using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    public event EventHandler OnResourceAmountChanged;

    private void Awake()
    {
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resources = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach(ResourceTypeSO resource in resources.list)
        {
            resourceAmountDictionary[resource] = 0;
        }

        //TestLogResources();
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ResourceTypeListSO resources = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
    //        AddResource(resources.list[0], 2);
    //        TestLogResources();
    //    }
    //}

    void TestLogResources()
    {
        foreach(ResourceTypeSO resource in resourceAmountDictionary.Keys)
        {
            Debug.Log(resource.nameString + ": " + resourceAmountDictionary[resource]);
        }
    }

    public void AddResource(ResourceTypeSO resource, int amount)
    {
        resourceAmountDictionary[resource] += amount;
        //TestLogResources();
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResource(ResourceTypeSO resource)
    {
        return resourceAmountDictionary[resource];
    }

    public bool CanAfford(ResourceAmount[] resourceAmounts)
    {
        foreach(ResourceAmount resourceAmount in resourceAmounts)
        {
            if (GetResource(resourceAmount.resourceType) > resourceAmount.amount)
            {
                //Can afford
            }
            else
            {
                //Can't afford
                return false;
            }
        }
        // Can afford
        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount resourceAmount in resourceAmounts)
        {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}
