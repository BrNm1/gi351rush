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
    public Animator animator;
    public Player player;
    public AudioSource jumpAudioSource;
    public AudioClip jumpSound;

    //public GameObject playerRun;
    //public GameObject playerSlide;
    
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
        if (player.die == true)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (player.die == false)
        {
            // ตรวจสอบว่าตัวละครอยู่บนพื้นหรือไม่
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            // เคลื่อนที่ไปข้างหน้า
            /*if (!isSliding)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }*/
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            
            // กระโดดเมื่อกดปุ่ม Space และอยู่บนพื้น
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
                animator.SetBool("Jump", true);
                animator.SetBool("Sliding", false);
                jumpAudioSource.PlayOneShot(jumpSound);
                //Debug.Log("Jump");
            }
        
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            // สไลด์เมื่อกดปุ่ม Shift และอยู่บนพื้น
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartSlide();
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier * 2 - 1) * Time.deltaTime;
                animator.SetBool("Sliding", true);
                animator.SetBool("Jump", false);
                //Debug.Log("Slide");
            }

            // จัดการกับการหมดเวลาในการสไลด์
            if (isSliding && Time.time > slideTime)
            {
                animator.SetBool("Sliding", false);
                StopSlide();
            }
        
            // รีเซ็ตสถานะการกระโดดเมื่อถึงพื้น
            if (isGrounded)
            {
                isJumping = false;
            
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                animator.SetBool("Jump", false);
            }
        }
        
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTime = Time.time + slideDuration;

        // ปรับขนาดของ BoxCollider2D เมื่อเริ่มสไลด์
        boxCollider.size = slideColliderSize;

        rb.velocity = new Vector2(slideSpeed, rb.velocity.y);
        
        //playerRun.SetActive(false);
        //playerSlide.SetActive(true);
    }

    private void StopSlide()
    {
        isSliding = false;

        // คืนขนาดของ BoxCollider2D เป็นขนาดปกติ
        boxCollider.size = normalColliderSize;

        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        
        //playerRun.SetActive(true);
        //playerSlide.SetActive(false);
    }
    
}
