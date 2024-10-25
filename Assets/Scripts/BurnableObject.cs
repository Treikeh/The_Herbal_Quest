using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableObject : MonoBehaviour
{
    public float durnDuration = 3f;
    private bool isBurning = false;

    public void StartBurning()
    {
        if (isBurning != true)
        {
            StartCoroutine(BurnOverTime());
        }
    }

    private IEnumerator BurnOverTime()
    {
        Debug.Log("Start burning");
        yield return new WaitForSeconds(durnDuration);
        gameObject.SetActive(false);
    }
}
