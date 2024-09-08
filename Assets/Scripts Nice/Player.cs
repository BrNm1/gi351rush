using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float delayBeforeGameOver = 5f;
    private int random;
    private float normalizedEnergy;
    
    public bool die = false;
    public Color originalColor;
    public SpriteRenderer spriteRenderer;
    public EnemyAI enemyAI;
    public Movement playerMovement;
    public Energybar energyBar;
    public float blinkDuration = 0.2f;
    public int blinkCount = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        energyBar.currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
        random = Random.Range(2, 6);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (energyBar.currentEnergy <= 0)
        {
            if (playerMovement != null)
            {
                die = true;
                //playerMovement.enabled = false;
            }
            
            if (enemyAI != null)
            {
                enemyAI.minDistance = -2;
            }
            
            StartCoroutine(HandleGameOver());
        }
        
        if (enemyAI != null && energyBar.currentEnergy > 0)
        {
            normalizedEnergy = energyBar.currentEnergy / maxEnergy;
            // คำนวณ minDistance ใหม่ตามระดับพลังงาน
            enemyAI.minDistance = Mathf.Lerp(1f, 5f, 1 - normalizedEnergy);
        }
    }
    
    public IEnumerator BlinkEffect()
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

    
    // Coroutine สำหรับหน่วงเวลาและโหลดฉากใหม่
    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(delayBeforeGameOver);
        SceneManager.LoadSceneAsync(5);
    }
}
