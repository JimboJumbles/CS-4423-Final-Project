using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Collider2D[] contactpoints;

    void Start()
    {
        StartCoroutine(laserLifetimeRoutine());
    }

    IEnumerator laserLifetimeRoutine(){
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        yield return null;
    }

    void OnCollisionEnter2D(Collision2D collision){
        // if(collision.gameObject.tag != "Player") Destroy(this.gameObject);
        Destroy(this.gameObject);
    }
}
