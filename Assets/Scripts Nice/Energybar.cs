using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energybar : MonoBehaviour
{
    public static Energybar Instance { get; private set; }
    public Slider slider;
    public float currentEnergy;

    private void Awake()
    {
        // Check if instance already exists and destroy duplicate if found
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
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
