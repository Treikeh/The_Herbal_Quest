using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Lvl3LightningStrike : MonoBehaviour
{
    [SerializeField] private GameObject lightningImage;
    [SerializeField] private Light lightningLight;

    private void Start()
    {
        lightningImage.SetActive(false);
    }

    public void TriggerLightningStrike()
    {
        lightningImage.SetActive(true);
    }
}
