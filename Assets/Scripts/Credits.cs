using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject mainCredits;
    [SerializeField] private GameObject assetsCredits;

    public void OnAssetCreditsButtonPressed()
    {
        mainCredits.SetActive(false);
        assetsCredits.SetActive(true);
    }

    public void OnMainCreditsButtonPressed()
    {
        mainCredits.SetActive(true);
        assetsCredits.SetActive(false);
    }
}
