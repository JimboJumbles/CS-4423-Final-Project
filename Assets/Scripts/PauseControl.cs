using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{

    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void pause(){
        isPaused = true;
        if (isPaused) Time.timeScale = 0;
        
    }

    public void unpause(){
        isPaused = false;
        Time.timeScale = 1;
        
    }
}
