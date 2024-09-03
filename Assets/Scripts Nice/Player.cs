using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxEnergy = 100f;
    private int random;
    
    public Energybar energyBar;
    // Start is called before the first frame update
    void Start()
    {
        energyBar.currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
        random = Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (energyBar.currentEnergy <= 0)
        {
            SceneManager.LoadSceneAsync(random);
        }
        
    }
}
