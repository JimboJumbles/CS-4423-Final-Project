using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseHandler;
    public GameObject OptionsMenu;
    bool pauseMenuOpen = false;

    public void pressPauseButton(){
        if (!pauseMenuOpen) openPauseMenu();
        else closePauseMenu();
    }

    public void openPauseMenu(){
        pauseMenuOpen = true;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        PauseHandler.GetComponent<PauseControl>().pause();
    }

    public void closePauseMenu(){
        pauseMenuOpen = false;
        transform.position = new Vector3(0, -10, 0);
        OptionsMenu.transform.position = new Vector3(0,-10,0);
        PauseHandler.GetComponent<PauseControl>().unpause();
    }

    public void openOptionsMenu(){
        OptionsMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        transform.position = new Vector3(0, -10, 0);
    }

    public void QuitButton(){
        Application.Quit();
    }
}
