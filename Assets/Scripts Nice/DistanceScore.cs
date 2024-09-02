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

    void Start()
    {
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<Movement>(); // รับค่า Movement
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
    }

    void UpdateScoreDisplay()
    {
        // แสดงระยะทางที่วิ่งได้ใน UI
        scoreText.text = "Score: " + distanceCovered.ToString("F0");
    }
    
    void AdjustPlayerAbilities()
    {
        // ใช้คะแนนระยะทางเพื่อปรับความเร็วและความสูงการกระโดด
        playerController.moveSpeed = 5f + (distanceCovered / 100f);
        playerController.slideSpeed = 7f + (distanceCovered / 100f);
        energyBar.currentEnergy = energyBar.currentEnergy - (distanceCovered / 100000f);
    }
}
