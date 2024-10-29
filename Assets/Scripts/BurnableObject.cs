using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BurnableObject : MonoBehaviour
{
    [SerializeField] private float BurnDuration = 3f;
    private bool _isBurning = false;

    public UnityEvent OnStartBurning;


public void Hit(bool startBurning)
    {
        if (startBurning && !_isBurning)
        {
            _isBurning = true;
            OnStartBurning.Invoke();
            Invoke(nameof(BurningFinished), BurnDuration);
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
