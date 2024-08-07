using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform buildingConstructionTransform = Instantiate(GameAssets.Instance.pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);
        return buildingConstruction;
    }

    private float constructionTimer;
    private float constructionTimerMax;
    private BuildingTypeSO buildingType;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;

        //Instantiate(GameAssets.Instance.pfBuildingConstruction, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalize());
        if(constructionTimer < 0)
        {
            constructionTimer += constructionTimerMax;
            Instantiate(GameAssets.Instance.pfBuildingPlacedParticles, transform.position, Quaternion.identity);
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        this.buildingType = buildingType;

        this.constructionTimerMax = buildingType.constructionTimerMax;
        this.constructionTimer = this.constructionTimerMax;

        this.spriteRenderer.sprite = buildingType.sprite;

        this.boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        this.boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;

        this.buildingTypeHolder.buildingType = buildingType;
    }

    public float GetConstructionTimerNormalize()
    {
        return 1-  this.constructionTimer / this.constructionTimerMax;
    }
}
