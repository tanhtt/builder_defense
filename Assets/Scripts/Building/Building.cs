using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;

    private void Awake()
    {
        buildingDemolishBtn = transform.Find("pfBuildingDemolish");
        buildingRepairBtn = transform.Find("pfBuildingRepair");
        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();
    }
    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem = GetComponent<HealthSystem>();

        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);

        healthSystem.OnDied += HealthSystem_OnDied;

        healthSystem.OnDamaged += HealthSystem_OnDamaged;

        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        if (healthSystem.IsFullHealth())
        {
            HideBuildingRepairBtn();
        }
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        ShowBuildingRepairBtn();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishBtn();
        //ShowBuildingRepairBtn();
    }

    private void OnMouseExit()
    {
        HideBuildingDemolishBtn();
        //HideBuildingRepairBtn();
    }

    private void ShowBuildingDemolishBtn()
    {
        if (buildingDemolishBtn == null) return;
        buildingDemolishBtn.gameObject.SetActive(true);
    }

    private void HideBuildingDemolishBtn()
    {
        if (buildingDemolishBtn == null) return;
        buildingDemolishBtn.gameObject.SetActive(false);
    }

    private void ShowBuildingRepairBtn()
    {
        if (buildingRepairBtn == null) return;
        buildingRepairBtn.gameObject.SetActive(true);
    }

    private void HideBuildingRepairBtn()
    {
        if (buildingRepairBtn == null) return;
        buildingRepairBtn.gameObject.SetActive(false);
    }
}
