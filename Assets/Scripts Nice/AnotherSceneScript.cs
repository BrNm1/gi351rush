using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnotherSceneScript : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        float distanceInAnotherScene = GameData.Instance.distanceCovered;
        scoreText.text = "Score: " + GameData.Instance.distanceCovered.ToString("F0");
        highScoreText.text = "High Score: " + GameData.Instance.highScore.ToString("F0");
    }
}
