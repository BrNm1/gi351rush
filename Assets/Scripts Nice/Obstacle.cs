using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Energybar energyBar;
    public float damage;
    public Player player;
    
    private Transform playerTransform;
    private float despawnDistance;
    
    public void Initialize(Transform playerTransform, float despawnDistance)
    {
        this.playerTransform = playerTransform;
        this.despawnDistance = despawnDistance;

        if (player == null)
        {
            player = playerTransform.GetComponent<Player>();
        }
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

            if (player != null && player.spriteRenderer != null)
            {
                StartCoroutine(player.BlinkEffect());
            }
        }
    }
    
    public void SetEnergyBar(Energybar newEnergyBar)
    {
        energyBar = newEnergyBar;
    }
    
}
