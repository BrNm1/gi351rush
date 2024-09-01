using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public Text scoreText; // ช่องที่จะแสดงคะแนน
    private int coin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        coin = 0;
        UpdateScoreDisplay();
    }

    public void AddScore(int points)
    {
        coin += points;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Coin: " + coin;
    }
}
