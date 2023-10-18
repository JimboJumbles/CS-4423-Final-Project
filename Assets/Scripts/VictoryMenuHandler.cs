using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuHandler : MonoBehaviour
{
    public void MenuButton(){
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevelButton(){
        SceneManager.LoadScene("Level1");
    }
}
