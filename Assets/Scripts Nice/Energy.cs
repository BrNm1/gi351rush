using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float energyAmount = 10f;
    public Energybar energyBar;

    private void Start()
    {
        energyBar = Energybar.Instance;

        if (energyBar == null)
        {
            Debug.Log("Energybar is null");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // เพิ่มคะแนนเมื่อชนกับ Player
            //CoinManager.Instance.AddScore(20); // เพิ่มคะแนน 1
            Destroy(gameObject); // ทำลายเหรียญ
            Debug.Log("GiveEnergy 10");
            energyBar.GiveEnergy(10f);
        }
    }
}
