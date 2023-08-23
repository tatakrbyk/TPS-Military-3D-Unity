using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healtBarSlider;

    public void GiveFullHealth(float health)
    {
        healtBarSlider.maxValue = health;
        healtBarSlider.value = health;
    }
    public void SetHealth(float health)
    {
        healtBarSlider.value = health;
    }
}
