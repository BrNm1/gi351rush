using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxEnergy = 100f;

    
    public Energybar energyBar;
    // Start is called before the first frame update
    void Start()
    {
        energyBar.currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
