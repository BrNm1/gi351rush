using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energybar : MonoBehaviour
{
    public Slider slider;
    public float currentEnergy;

    private void Update()
    {
        if (slider.value != currentEnergy)
        {
            slider.value = currentEnergy;
        }

        if (currentEnergy > 100)
        {
            currentEnergy = 100;
            slider.value = currentEnergy;
        }
    }

    public void SetMaxEnergy(float energy)
    {
        slider.maxValue = energy;
        slider.value = energy;
    }
    public void SetEnergy(float energy)
    {
        slider.value = energy;
    }
    
    public void TakeEnergy(float damage)
    {
        currentEnergy -= damage;
        SetEnergy(currentEnergy);
    }

    public void GiveEnergy(float getEnergy)
    {
        currentEnergy += getEnergy;
        SetEnergy(currentEnergy);
    }
}
