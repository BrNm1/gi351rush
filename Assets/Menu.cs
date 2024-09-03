using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void LoadToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void ExitGame()
    { 
        Application.Quit();
    }
    
}
