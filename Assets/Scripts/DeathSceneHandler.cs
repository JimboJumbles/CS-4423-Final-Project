using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneHandler : MonoBehaviour
{
    public void PlayAgain(){
        SceneManager.LoadScene("Level1");
    }

    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}