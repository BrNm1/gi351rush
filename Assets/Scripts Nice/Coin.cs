using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Energybar energyBar;
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
