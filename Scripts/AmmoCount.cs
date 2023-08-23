using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammunitionText;
    [SerializeField] private TextMeshProUGUI magText;

    public static AmmoCount Instance { get; private set; }
    private void Awake()
    {
       Instance = this;
    }

    public void UpdateAmmoText(int presentAmmunition)
    {
        ammunitionText.text = "   \\ " + presentAmmunition;
    }
    public void UpdateMagText(int mag)
    {
        magText.text = "" + mag;
    }

}
