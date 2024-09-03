using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Energybar energyBar;
    public float damage;
    private Transform playerTransform;
    private float despawnDistance;
    
    void Update()
    {
        // ตรวจสอบระยะห่างจาก Player และทำลายถ้าจำเป็น
        /*if (playerTransform != null)
        {
            float distanceToPlayer = Mathf.Abs(playerTransform.position.x - transform.position.x);
            if (distanceToPlayer > despawnDistance)
            {
                Destroy(gameObject);
            }
        }*/
    }
    
    public void Initialize(Transform playerTransform, float despawnDistance)
    {
        this.playerTransform = playerTransform;
        this.despawnDistance = despawnDistance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (energyBar != null)
            {
                energyBar.TakeEnergy(damage);
                Debug.Log("TakeEnergy " + damage);
            }
            else
            {
                Debug.LogWarning("Energybar is not assigned in Obstacle script.");
            }
        }
    }
    
    public void SetEnergyBar(Energybar newEnergyBar)
    {
        energyBar = newEnergyBar;
    }
}
