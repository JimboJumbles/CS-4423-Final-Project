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
    float centerPosition;

    // Start is called before the first frame update
    void Start()
    {
        coinCollider = GetComponent<BoxCollider2D>();
        direction = "up";
        player = GameObject.FindWithTag("Player");
        ground = GameObject.FindWithTag("Ground");
        coinCounter = GameObject.FindWithTag("CoinCounter").GetComponent<CoinCounter>();
        groundCollider = ground.GetComponent<CompositeCollider2D>();
        centerPosition = transform.position.y;
    }

    void Update(){
        if (direction == "up"){
                    if (transform.position.y > (centerPosition + 0.3f)) direction = "down";
                    else transform.position += new Vector3(0, 0.005f, 0);
        }
        else if (direction == "down"){
                    if (transform.position.y < (centerPosition - 0.3f)) direction = "up";
                    else transform.position += new Vector3(0, -0.005f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            coinCounter.getCoin();
            Destroy(this.gameObject);
        }
    }
}
