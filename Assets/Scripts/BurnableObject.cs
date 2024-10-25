using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BurnableObject : MonoBehaviour, IHitable
{
    public float durnDuration = 3f;
    public UnityEvent OnStartBurning;

    private bool isBurning = false;


public void Hit(bool startBurning)
    {
        if (startBurning && !isBurning)
        {
            StartBurning();
        }
        else
        {
            Debug.Log("Torch not lit or object allready burning");
        }
    }

    public void StartBurning()
    {
        isBurning = true;
        OnStartBurning.Invoke();
        StartCoroutine(BurningCorutine());
    }

    private IEnumerator BurningCorutine()
    {
        Debug.Log("Start burning");
        yield return new WaitForSeconds(durnDuration);
        gameObject.SetActive(false);
    }
}
