using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAberration : MonoBehaviour
{
    public static ChromaticAberration Instance { get; private set; }
    private Volume volume;
    private float timer;

    private void Awake()
    {
        Instance = this;
        volume = GetComponent<Volume>();
    }

    private void Update()
    {
        if(volume.weight > 0)
        {
            volume.weight -= Time.deltaTime;
        }
    }

    public void SetWeight(float weight)
    {
        volume.weight = weight;  
    }
}
