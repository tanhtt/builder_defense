using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform bar;

    private void Start()
    {
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGenerator.GetResourceGeneratorData().resourceType.sprite;
        bar = transform.Find("bar");

        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {
        bar.localScale = new Vector3( 1 - resourceGenerator.GetTimerNormalize(), 1, 1);
    }
}
