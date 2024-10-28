using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BurnableObject : MonoBehaviour
{
    public float burnDuration = 3f;
    public UnityEvent OnStartBurning;

    private bool isBurning = false;


public void Hit(bool startBurning)
    {
        if (startBurning && !isBurning)
        {
            isBurning = true;
            OnStartBurning.Invoke();
            Invoke(nameof(BurningFinished), burnDuration);
        }
        else
        {
            Debug.Log("Torch not lit or object allready burning");
        }
    }

    private void BurningFinished()
    {
        gameObject.SetActive(false);
    }
}
