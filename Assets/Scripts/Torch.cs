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
            flameLight.enabled = true;
            playerData.torchLit = true;
            // Play sound
        }
    }

    public void ExtinguishTorch()
    {
        if (flameLight.enabled == true)
        {
            flameLight.enabled = false;
            playerData.torchLit = false;
            // Play sound
        }
    }
}
