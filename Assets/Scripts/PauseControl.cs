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
        Time.timeScale = 0;
        isPaused = true;
    }

    public void unpause(){
        Time.timeScale = 1;
        isPaused = false;
    }
}
