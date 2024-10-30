using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BurnableObject : MonoBehaviour
{
    [SerializeField] private float burnDuration = 3f;
    private bool isBurning = false;

    public UnityEvent OnStartBurning;


    public void RegisterHit(bool onFire, Transform hitterTransform)
    {
        if (onFire && !isBurning)
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
