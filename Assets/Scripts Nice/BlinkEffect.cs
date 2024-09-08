using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public Color originalColor;
    public SpriteRenderer spriteRenderer;
    public float blinkDuration = 0.2f;
    public int blinkCount = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (spriteRenderer != null)
            {
                StartCoroutine(BEffect());
            }
        }
    }

    public IEnumerator BEffect()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.color = Color.red;
            Debug.Log("ON Blink Effect");
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = originalColor;
            Debug.Log("OFF Blink Effect");
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
