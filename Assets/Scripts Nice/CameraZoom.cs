using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform player; // ตัวละครที่จะติดตาม
    public float jumpPositionZ = -15f; // ตำแหน่ง Z เมื่อกระโดด
    public float normalPositionZ = -13f; // ตำแหน่ง Z ปกติ
    public float smoothSpeed = 0.1f; // ความราบรื่นในการเปลี่ยนตำแหน่ง
    public float smoothTime = 0.1f; // ความราบรื่นในการเปลี่ยนแปลงตำแหน่ง (ค่าต่ำจะทำให้การเปลี่ยนแปลงเร็วขึ้น)
    private Vector3 velocity = Vector3.zero; // ใช้ในการเก็บข้อมูลความเร็ว
    private Camera cam;
    private bool isJumping;

    private void Start()
    {
        cam = Camera.main; // ใช้กล้องหลักของฉาก
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, normalPositionZ); // ตั้งค่าตำแหน่ง Z เริ่มต้น
    }

    private void Update()
    {
        // ตรวจสอบการกระโดด
        if (Input.GetKey(KeyCode.Space)) // ใช้ปุ่ม "Jump" สำหรับกระโดด
        {
            isJumping = true;
            StopAllCoroutines(); // หยุดการเปลี่ยนตำแหน่ง Z ปกติ
            StartCoroutine(MoveIn()); // เริ่มการเปลี่ยนตำแหน่ง Z
        }

        // ตรวจสอบว่าตัวละครลงพื้น
        if (isJumping && Mathf.Approximately(player.GetComponent<Rigidbody2D>().velocity.y, 0))
        {
            isJumping = false;
            StartCoroutine(MoveOut()); // กลับมาที่ตำแหน่ง Z ปกติ
        }
    }
    
    private IEnumerator MoveIn()
    {
        Vector3 targetPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, jumpPositionZ);
        while (Mathf.Abs(cam.transform.position.z - jumpPositionZ) > 0.01f)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, smoothSpeed);
            yield return null;
        }
        cam.transform.position = targetPosition;
    }

    private IEnumerator MoveOut()
    {
        Vector3 targetPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, normalPositionZ);
        while (Mathf.Abs(cam.transform.position.z - normalPositionZ) > 0.01f)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, smoothSpeed);
            yield return null;
        }
        cam.transform.position = targetPosition;
    }
}
