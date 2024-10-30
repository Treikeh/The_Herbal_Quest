using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitable : MonoBehaviour
{
    public UnityEvent<bool, Transform> OnHit;


    public void Hit(bool onFire, Transform hitterTransform)
    {
        OnHit.Invoke(onFire, hitterTransform);
    }
}
