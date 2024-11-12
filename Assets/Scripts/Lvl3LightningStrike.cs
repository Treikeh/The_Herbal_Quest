using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Lvl3LightningStrike : MonoBehaviour
{
    [SerializeField] private Material lightningMaterial;
    [SerializeField] private Light lightningLight;

    private bool triggerd = false;

    private void Update()
    {
        if (triggerd)
        {
            var temp = lightningMaterial.color;
            temp.a -= Time.deltaTime / 1.5f;
            lightningMaterial.color = temp;
            lightningLight.intensity -= Time.deltaTime * 30f;
        }
    }

    public void TriggerLightningStrike()
    {
        lightningMaterial.color = Color.white;
        lightningLight.intensity = 40f;
        triggerd = true;
    }
}
