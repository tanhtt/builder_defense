using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : Singleton<BuildingManager>
{
    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(activeBuildingType != null && CanSpawnBuilding(activeBuildingType, UtilsClass.MouseWorldPosition()))
            {
                if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                {
                    ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                    Instantiate(activeBuildingType.prefab, UtilsClass.MouseWorldPosition(), Quaternion.identity);
                }
            }
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        this.activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this.tag, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType }); 
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return this.activeBuildingType;
    }

    public bool CanSpawnBuilding(BuildingTypeSO building, Vector3 position)
    {
        BoxCollider2D boxCollider2D = building.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] col2DArr = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isClearArea = col2DArr.Length == 0;
        if (!isClearArea) return false;

        col2DArr = Physics2D.OverlapCircleAll(position, building.minConstructionRadius);
        foreach(Collider2D collider2d in col2DArr)
        {
            // Col inside the constructions radius
            BuildingTypeHolder buildingTypeHolder = collider2d.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                // Has a building type
                if(buildingTypeHolder.buildingType == building)
                {
                    // There already a building of this type within the construction radius!
                    return false;
                }
            }
        }

        float maxConstructionRadius = 25;
        col2DArr = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider2d in col2DArr)
        {
            // Col inside the constructions radius
            BuildingTypeHolder buildingTypeHolder = collider2d.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                // Has a building type
                return true;
            }
        }

        return false;
    }
}
