using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneHandler : MonoBehaviour
{
    [SerializeField] int curLevel;
    [SerializeField] GameObject BGMHandler;
    [SerializeField] GameObject pauseHandler;

    void OnEnable(){
        BGMHandler.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        GetComponentInParent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        pauseHandler.GetComponent<PauseControl>().pause();
        Cursor.visible = true;
    }

    public void PlayAgain(){
        if (curLevel == 1) SceneManager.LoadScene("Level1");
        else if (curLevel == 2) SceneManager.LoadScene("Level2");
        else if (curLevel == 3) SceneManager.LoadScene("Level3");
    }

    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
