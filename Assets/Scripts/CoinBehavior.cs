using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    GameObject player;
    GameObject ground;
    Collider2D coinCollider;
    Collider2D groundCollider;
    CoinCounter coinCounter;
    string direction;

    // Start is called before the first frame update
    void Start()
    {
        coinCollider = GetComponent<BoxCollider2D>();
        direction = "up";
        player = GameObject.FindWithTag("Player");
        ground = GameObject.FindWithTag("Ground");
        coinCounter = player.GetComponent<CoinCounter>();
        groundCollider = ground.GetComponent<CompositeCollider2D>();
    }

    void Update(){
        switch (direction){
            case ("up"):
                if (coinCollider.Distance(groundCollider).distance >= 1) direction = "down";
                else transform.position += new Vector3(0, 0.005f, 0);
                break;
            case ("down"):
                if (coinCollider.Distance(groundCollider).distance <= 0.5) direction = "up";
                else transform.position += new Vector3(0, -0.005f, 0);
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            coinCounter.getCoin();
            Destroy(this.gameObject);
        }
    }
}
