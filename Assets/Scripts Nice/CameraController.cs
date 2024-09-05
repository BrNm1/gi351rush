using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // ตัวละครที่จะติดตาม
    public float smoothSpeed = 1f; // ความราบรื่นในการเปลี่ยนตำแหน่ง
    public Vector3 offset; // ตำแหน่งเริ่มต้น

    public float jumpPositionZ = -15f; // ตำแหน่ง Z เมื่อกระโดด
    public float normalPositionZ = -13f; // ตำแหน่ง Z ปกติ
    public float smoothZSpeed = 0.1f; // ความราบรื่นในการเปลี่ยนแปลงตำแหน่ง Z
    private Camera cam;
    private bool isJumping;

    private void Start()
    {
        cam = Camera.main; // ใช้กล้องหลักของฉาก
        // ตั้งค่าตำแหน่ง Z เริ่มต้น
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, normalPositionZ);
    }

    private void Update()
    {
        // ตรวจสอบการกระโดด
        if (Input.GetKey(KeyCode.Space)) // ใช้ปุ่ม "Jump" สำหรับกระโดด
        {
            if (!isJumping) // ตรวจสอบว่าไม่ได้อยู่ในสถานะกระโดดอยู่แล้ว
            {
                isJumping = true;
                StopAllCoroutines(); // หยุดการเปลี่ยนตำแหน่ง Z ปกติ
                StartCoroutine(MoveZ(jumpPositionZ)); // เริ่มการเปลี่ยนตำแหน่ง Z
            }
        }
        
        // ตรวจสอบว่าตัวละครลงพื้น
        if (isJumping)
        {
            isJumping = false;
            StartCoroutine(MoveZ(normalPositionZ)); // กลับมาที่ตำแหน่ง Z ปกติ
        }
    }

    private void LateUpdate()
    {
        // ติดตามตำแหน่งของตัวละคร
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private IEnumerator MoveZ(float targetZ)
    {
        Vector3 targetPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, targetZ);
        while (Mathf.Abs(cam.transform.position.z - targetZ) > 0.01f)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, smoothZSpeed);
            yield return null;
        }
        cam.transform.position = targetPosition;
    }
}
