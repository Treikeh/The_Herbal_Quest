using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public PlayerData playerData;
    public Light flameLight;

    public void LightTorch()
    {
        if (flameLight.enabled == false)
        {
            playerData.attackDamage = playerData.DefaultDamage;
            flameLight.enabled = true;
            // Play sound
        }
    }

    public void ExtinguishTorch()
    {
        if (flameLight.enabled == true)
        {
            playerData.attackDamage = 0;
            flameLight.enabled = false;
            // Play sound
        }
    }
}
