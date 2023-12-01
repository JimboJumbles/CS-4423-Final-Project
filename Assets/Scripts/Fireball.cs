using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0,20,0);
    }

    void OnTriggerEnter2D(Collider2D player){
        if(player.tag == "Player"){
            player.GetComponent<PlayerHealth>().damagePlayer(1);
        }
    }
}
