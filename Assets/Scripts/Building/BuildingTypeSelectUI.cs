using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrorSprite;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingType;
    private Transform arrorButton;
    private BuildingTypeListSO buildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> buildingTypeTransDict;
    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        buildingTypeTransDict = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        arrorButton = Instantiate(btnTemplate, transform);
        arrorButton.gameObject.SetActive(true);

        float offsetAmount = 112f;
        arrorButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
        
        arrorButton.Find("image").GetComponent<Image>().sprite = arrorSprite;
        arrorButton.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);

        arrorButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        }
        );

        MouseEnterExitEvents mouseEnterExitEvents = arrorButton.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Show("Arror");
        };
        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Hide();
        };

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            if(ignoreBuildingType.Contains(buildingType)) continue;
            Transform buildingTypeTransform = Instantiate(btnTemplate, transform);
            buildingTypeTransform.gameObject.SetActive(true);

            offsetAmount = 112f;
            buildingTypeTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            index++;

            buildingTypeTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            buildingTypeTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            }
            );

            mouseEnterExitEvents = buildingTypeTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetConstructionResourceCostString());
            };
            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };

            buildingTypeTransDict[buildingType] = buildingTypeTransform;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateSelectedButton();
    }

    private void UpdateSelectedButton()
    {
        arrorButton.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in buildingTypeTransDict.Keys)
        {
            Transform buildingTypeTransform = buildingTypeTransDict[buildingType];
            buildingTypeTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if (activeBuildingType == null)
        {
            arrorButton.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            buildingTypeTransDict[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}
