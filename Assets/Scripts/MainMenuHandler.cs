using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void PlayGame(){
        Cursor.visible = true;
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
