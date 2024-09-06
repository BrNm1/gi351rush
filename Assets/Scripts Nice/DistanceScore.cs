using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceScore : MonoBehaviour
{
    public Text scoreText; // ช่องที่จะแสดงคะแนน
    public float distanceCovered; // ระยะทางที่วิ่งได้
    private Vector2 lastPosition; // ตำแหน่งล่าสุดของตัวละคร
    public float moveSpeed = 5f; // ความเร็วการวิ่ง
    public float jumpForce = 10f; // ความสูงการกระโดด
    
    private Rigidbody2D rb;
    private Movement playerController;
    public Energybar energyBar;
    public GameManager gameManager; // เชื่อมโยง ObstacleSpawner
    
    void Start()
    {
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<Movement>(); // รับค่า Movement
        gameManager = FindObjectOfType<GameManager>(); // ค้นหา ObstacleSpawner ใน Scene
    }

    void Update()
    {
        // คำนวณระยะทางที่วิ่ง
        float distance = Vector2.Distance(lastPosition, transform.position);
        distanceCovered += distance;

        // อัพเดตตำแหน่งล่าสุด
        lastPosition = transform.position;

        // อัพเดตคะแนนใน UI
        UpdateScoreDisplay();
        
        // ปรับความเร็วการวิ่งและความสูงการกระโดดตามคะแนน
        AdjustPlayerAbilities();

        GameData.Instance.distanceCovered = distanceCovered;
        
        // ส่งความเร็วปัจจุบันไปยัง ObstacleSpawner
        if (gameManager != null)
        {
            gameManager.UpdateSpawnInterval(1f / moveSpeed); // ใช้ความเร็วเพื่ออัพเดตช่วงเวลาการสร้าง
        }
    }

    void UpdateScoreDisplay()
    {
        // แสดงระยะทางที่วิ่งได้ใน UI
        scoreText.text = "Score: " + distanceCovered.ToString("F0");
    }
    
    void AdjustPlayerAbilities()
    {
        // ใช้คะแนนระยะทางเพื่อปรับความเร็วและความสูงการกระโดด
        playerController.moveSpeed = 10f + (distanceCovered / 50f);
        playerController.slideSpeed = 10f + (distanceCovered / 50f);
        energyBar.currentEnergy = energyBar.currentEnergy - (distanceCovered / 100000f);
    }
}
