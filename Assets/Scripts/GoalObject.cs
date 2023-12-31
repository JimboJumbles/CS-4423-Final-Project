using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalObject : MonoBehaviour
{

    [SerializeField] GameObject victoryMenu;
    [SerializeField] GameObject BGMHandler;
    [SerializeField] GameObject coinCounter;
    [SerializeField] int coinsRequired;
    [SerializeField] Image notEnoughCoinsMessage;

    BoxCollider2D objectCollider;
    Collider2D groundCollider;
    PauseControl pauseHandler;
    string direction;
    bool isLocked = true;
    

    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent<BoxCollider2D>();
        groundCollider = GameObject.FindWithTag("Ground").GetComponent<CompositeCollider2D>();
        pauseHandler = GameObject.FindWithTag("PauseHandler").GetComponent<PauseControl>();
        direction = "up";
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseHandler.isPaused){
            switch (direction){
                case ("up"):
                    if (objectCollider.Distance(groundCollider).distance >= 1) direction = "down";
                    else transform.position += new Vector3(0, 0.005f, 0);
                    break;
                case ("down"):
                    if (objectCollider.Distance(groundCollider).distance <= 0.5) direction = "up";
                    else transform.position += new Vector3(0, -0.005f, 0);
                    break;
                default:
                    break;
            }
        }

        if (coinCounter.GetComponent<CoinCounter>().numCoins >= coinsRequired){
            isLocked = false;
        }
        
    }

    void OnCollisionEnter2D(Collision2D player){
        if (isLocked){
            StartCoroutine(showMessageRoutine());
        }
        else{
            BGMHandler.GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().Play();
            victoryMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            pauseHandler.pause();
            Cursor.visible = true;
        }
        
    }

    IEnumerator showMessageRoutine(){
        notEnoughCoinsMessage.color = new Color(0, 0, 0, 0.75f);
        notEnoughCoinsMessage.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 1);

        yield return new WaitForSeconds(2);
        notEnoughCoinsMessage.color = new Color(0, 0, 0, 0);
        notEnoughCoinsMessage.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 0);
        yield return null;
    }
}
