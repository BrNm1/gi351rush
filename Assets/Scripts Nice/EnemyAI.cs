using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;        // อ้างอิงถึง Transform ของผู้เล่น
    public float speed = 5.0f;      // ความเร็วของศัตรู
    public float smoothTime = 0.3f; // เวลาที่ใช้ในการปรับเปลี่ยนตำแหน่ง
    public float minDistance = 2.5f;

    private Vector3 velocity = Vector3.zero; // ใช้ในการคำนวณการเคลื่อนที่

    private void Update()
    {
        if (player != null)
        {
            // คำนวณทิศทางจากศัตรูไปยังผู้เล่น
            Vector3 targetPosition = player.position;

            // คำนวณระยะทางปัจจุบันระหว่างศัตรูกับผู้เล่น
            float distance = Vector3.Distance(transform.position, targetPosition);

            // ตรวจสอบว่าระยะทางมากกว่าระยะขั้นต่ำ
            if (distance > minDistance)
            {
                // คำนวณตำแหน่งที่ควรจะเคลื่อนที่ไปในทิศทางที่เนียน
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);
            }
        }
    }
}
