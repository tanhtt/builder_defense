using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameobject;
    [SerializeField] private ResourceNearbyOverlay nearbyOverlay;

    private void Awake()
    {
        spriteGameobject = transform.Find("sprite").gameObject;
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if(e.activeBuildingType == null)
        {
            Hide();
            nearbyOverlay.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            if(e.activeBuildingType.hasResourceGeneratorData)
            {
                nearbyOverlay.Show(e.activeBuildingType.generatorData);
            }
            else
            {
                nearbyOverlay.Hide();
            }
        }
    }

    private void Update()
    {
        transform.position = UtilsClass.MouseWorldPosition();
    }

    private void Show(Sprite ghostSprite)
    {
        spriteGameobject.SetActive(true);
        spriteGameobject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide()
    {
        spriteGameobject.SetActive(false);
    }
}
