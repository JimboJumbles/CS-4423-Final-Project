using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuHandler : MonoBehaviour
{
    [SerializeField] int curLevel;

    public void MenuButton(){
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevelButton(){
        if (curLevel == 1) SceneManager.LoadScene("Level2");
        else if (curLevel == 2) SceneManager.LoadScene("Level3");
        else if (curLevel == 3) SceneManager.LoadScene("VictoryMenu");
        else Debug.Log("Load Failed for level " + curLevel);
    }
}
