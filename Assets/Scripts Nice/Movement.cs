using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 2.5f;
    public float fallMultiplier = 10f; // ค่าคูณความเร็วการตก
    public float slideSpeed = 7f;  // ความเร็วในการสไลด์
    public float slideDuration = 0.5f;  // ระยะเวลาในการสไลด์
    public Vector2 normalColliderSize; // ขนาดของ BoxCollider2D ปกติ
    public Vector2 slideColliderSize;  // ขนาดของ BoxCollider2D เมื่อสไลด์
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool isGrounded;
    private bool isSliding;
    private float slideTime;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        // บันทึกขนาดของ BoxCollider2D ปกติ
        normalColliderSize = boxCollider.size;
    }

    void Update()
    {
        // ตรวจสอบว่าตัวละครอยู่บนพื้นหรือไม่
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // เคลื่อนที่ไปข้างหน้า
        if (!isSliding)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        // กระโดดเมื่อกดปุ่ม Space และอยู่บนพื้น
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            Debug.Log("Jump");
        }
        
        // ปรับความเร็วในการตก
        if (rb.velocity.y < 0)
        {
            if (isJumping && isSliding)
            {
                // เพิ่มความเร็วการตกเมื่อสไลด์และกระโดด
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier * 2 - 1) * Time.deltaTime;
            }
            else
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }

        // สไลด์เมื่อกดปุ่ม Shift และอยู่บนพื้น
        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartSlide();
            Debug.Log("Slide");
        }

        // จัดการกับการหมดเวลาในการสไลด์
        if (isSliding && Time.time > slideTime)
        {
            StopSlide();
        }
        
        // รีเซ็ตสถานะการกระโดดเมื่อถึงพื้น
        if (isGrounded)
        {
            isJumping = false;
        }
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTime = Time.time + slideDuration;

        // ปรับขนาดของ BoxCollider2D เมื่อเริ่มสไลด์
        boxCollider.size = slideColliderSize;

        rb.velocity = new Vector2(slideSpeed, rb.velocity.y);
    }

    private void StopSlide()
    {
        isSliding = false;

        // คืนขนาดของ BoxCollider2D เป็นขนาดปกติ
        boxCollider.size = normalColliderSize;

        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

}
