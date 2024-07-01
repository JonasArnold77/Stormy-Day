using FMCOLOR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingManager : MonoBehaviour
{
    public Volume World;
    public Volume Bar;
    public FMColor _FMColor;
    public List<GameObject> LightGameObjects = new List<GameObject>();

    public static PostProcessingManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ActivateVolume(Volume volume, float amount)
    {
        while (volume.weight <= amount - 0.1f)
        {
            volume.weight = volume.weight + 0.1f;
            yield return new WaitForSeconds(0.25f);
        }

        volume.weight = amount;
    }

    public IEnumerator DeactivateVolume(Volume volume)
    {
        while (volume.weight >= 0)
        {
            volume.weight = volume.weight - 0.1f;
            yield return new WaitForSeconds(0.25f);
        }

        volume.weight = 0;
    }
}
