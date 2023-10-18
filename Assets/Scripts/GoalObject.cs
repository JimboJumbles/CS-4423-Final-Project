using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour
{

    [SerializeField] GameObject victoryMenu;

    BoxCollider2D objectCollider;
    Collider2D groundCollider;
    PauseControl pauseHandler;
    string direction;

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
        
    }

    void OnCollisionEnter2D(Collision2D player){
        victoryMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        pauseHandler.pause();
        Cursor.visible = true;
    }


}
